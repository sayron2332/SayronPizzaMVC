using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Bcpg.OpenPgp;
using SayronPizzaMVC.Core.AutoMappers.User;
using SayronPizzaMVC.Core.DTO_s;
using SayronPizzaMVC.Core.Entites.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SayronPizzaMVC.Core.Services
{
  
    public class UserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _autoMapper;
        private readonly EmailService _emailService;
        private readonly IConfiguration _config;
        public UserService(IConfiguration config,UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper autoMapper, EmailService emailService) 
        { 
            _userManager = userManager;
            _signInManager = signInManager;
            _autoMapper = autoMapper;
            _emailService = emailService;
            _config = config;
        }
        public async Task SendConfirmationEmail(AppUser newUser)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            var encodedEmailToken = Encoding.UTF8.GetBytes(token);
            var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

            var url = $"{_config["HostSettings:URL"]}/Dashboard/confirmemail?userid={newUser.Id}&token={validEmailToken}";
            string body = $"<h1>Confirm your email</h1> <a href='{url}'>Confirm now!</a>";
            await _emailService.SendEmailAsync(newUser.Email, "Confirmation email.", body);
        }
        public async Task<ServiceResponse> LoginUserAsync(LoginUserDto model)
        {
         
            var user = await _userManager.FindByEmailAsync(model.Email);
           
            if (user == null) 
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "User not found or email or password incorrect",
                };
            }
        
            SignInResult result = await _signInManager.PasswordSignInAsync(user, model.Password,model.RememberMe, lockoutOnFailure: true);
            if (result.Succeeded)
            {
             
                return new ServiceResponse 
                {
                    Success = true,

                    Message = "You SignIn",
                };
            }
            if (result.IsNotAllowed)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Confirm your email please."
                };
            }
            if (result.IsLockedOut)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "User is locked out. Connect with site administrator."
                };
            }

            return new ServiceResponse
            {
                Success = false,
                Message = "User or password incorrect."
            };
        }
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<ServiceResponse> GetByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return new ServiceResponse { Success = false, Message = "User not found." };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var mappedUser = _autoMapper.Map<AppUser, UsersDto>(user);
            mappedUser.Role = roles[0];

            return new ServiceResponse
            {
                Success = true,
                Message = "User loaded!",
                Payload = mappedUser
            };
        }
        public async Task<ServiceResponse> ResetPasswordAsync(ForgotPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "User not found."
                };
            }

            var decodedToken = WebEncoders.Base64UrlDecode(model.Token);
            var normalToken = Encoding.UTF8.GetString(decodedToken);


            var result = await _userManager.ResetPasswordAsync(user, normalToken, model.Password);

            if (result.Succeeded)
            {
                return new ServiceResponse
                {
                    Success = true,
                    Message = "Password successfully reset."
                };
            }

            return new ServiceResponse
            {
                Success = false,
                Message = "Something wrong. Connect with your admin.",
                Errors = result.Errors.Select(e => e.Description)
            };
        }
        public async Task<ServiceResponse> SendResetPasswordEmailAsync(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "The email isn`t exist",
                    Errors = new List<string>
                    {
                        "The email isn`t exist",
                    }
                };
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedEmailToken = Encoding.UTF8.GetBytes(token);
            var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

            var url = $"{_config["HostSettings:URL"]}/Dashboard/ResetPassword?email={Email}&token={validEmailToken}";
            await _emailService.SendEmailAsync(Email, "Reset Password email.", url);

            return new ServiceResponse
            {
                Success = true,
                Message = "Email for reset password successfully send."
            };

        }
        public async Task<ServiceResponse> GetAllAsync()
        {
            List<AppUser> users = await _userManager.Users.ToListAsync();
            List<UsersDto> mappedUsers = users.Select(u => _autoMapper.Map<AppUser, UsersDto>(u)).ToList();

            for (int i = 0; i < mappedUsers.Count; i++)
            {
                mappedUsers[i].Role = (await _userManager.GetRolesAsync(users[i])).FirstOrDefault();
            }
            return new ServiceResponse
            {
                Success = true,
                Message = "all users loaded",
                Payload = mappedUsers
            };
        }
        public async Task<ServiceResponse> DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "User not found."
                };
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return new ServiceResponse
                {
                    Success = true,
                    Message = "User successfully deleted."
                };
            }

            return new ServiceResponse
            {
                Success = false,
                Message = "Sonething wrong. Connect with your admin.",
                Errors = result.Errors.Select(e => e.Description)
            };
        }
        public async Task SendConfirmEmailAsync(AppUser NewUser)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(NewUser);
            var encodedEmailToken = Encoding.UTF8.GetBytes(token);
            var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

            var url = $"{_config["HostSettings:URL"]}/Dashboard/confirmemail?userid={NewUser.Id}&token={validEmailToken}";
            await _emailService.SendEmailAsync(NewUser.Email, "Confirmation email", url);
        }
        public async Task<ServiceResponse> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "User not found."
                };
            }

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            var normalToken = Encoding.UTF8.GetString(decodedToken);
            var result = await _userManager.ConfirmEmailAsync(user, normalToken);

            if (result.Succeeded)
            {
                return new ServiceResponse
                {
                    Success = true,
                    Message = "Email successfully confirmed."
                };
            }

            return new ServiceResponse
            {
                Success = false,
                Message = "Email not confirmed.",
                Errors = result.Errors.Select(e => e.Description)
            };
        }
        public async Task<ServiceResponse> CreateAsync(CreateUserDto model)
        { 
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "this user already exits"
                };
            }
            AppUser mappedUser = _autoMapper.Map<CreateUserDto, AppUser>(model);
            var result = await _userManager.CreateAsync(mappedUser);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(mappedUser, model.Role);
                await SendConfirmEmailAsync(mappedUser);

                return new ServiceResponse
                {
                    Success = true,
                    Message = "user successfully create"
                };
            }
            List<IdentityError> errorList = result.Errors.ToList();

            string errors = "";
            foreach (var error in errorList)
            {
                errors = errors + error.Description.ToString();
            }

            return new ServiceResponse
            {
                Message = "User creating error.",
                Success = false,
                Payload = errors
            };

        }

    }
}
