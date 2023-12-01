using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SayronPizzaMVC.Core.DTO_s;
using SayronPizzaMVC.Core.Entites.Category;
using SayronPizzaMVC.Core.Entites.User;
using SayronPizzaMVC.Core.Services;
using SayronPizzaMVC.Core.Validation.User;
using System.ComponentModel.DataAnnotations;

namespace SayronPizzaMVC.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class DashboardController : Controller
    {
        private readonly UserService _userService;
        
        public DashboardController(UserService userService)
        {
            _userService = userService;
            
        }
        public IActionResult Index()
        {
            
            return View();
        }
        [AllowAnonymous]
        public IActionResult SignIn()
        {
            var user = HttpContext.User.Identity.IsAuthenticated;
            if (user)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(LoginUserDto model)
        {
           
            LoginUserValidator validator = new LoginUserValidator();
            var validationResult = validator.Validate(model);
            if (validationResult.IsValid) 
            {
               var result = await _userService.LoginUserAsync(model);
               if (result.Success)
               {
                    return RedirectToAction(nameof(Index));
               }
                ViewBag.Errors = result.Message;
                return View("SignIn");
            }
            ViewBag.Errors = validationResult.Errors[0];
            return View("SignIn");
        }
        public async Task<IActionResult> Logout(LoginUserDto model)
        {
            await _userService.LogoutAsync();
            return RedirectToAction(nameof(SignIn));
        }

        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View("ForgotPassword");
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            ServiceResponse result = await _userService.SendResetPasswordEmailAsync(Email);
            if (result.Success)
            {
                ViewBag.Errors = "Check your Email";
                return View("SignIn");
            }
            ViewBag.Errors = result.Message;
            return View();
            
        }
        public async Task<IActionResult> Profile(string id)
        {
            var result = await _userService.GetByIdAsync(id);
            if (result.Success)
            {
                return View(result.Payload);
            }
            return View();
        }
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(string email, string token)
        {
            @ViewBag.Email = email;
            @ViewBag.Token = token;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ForgotPasswordDto model)
        {

            ForgotPasswordValidator validator = new ForgotPasswordValidator();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                var result = await _userService.ResetPasswordAsync(model);
                if (result.Success)
                {
                    ViewBag.Errors = result.Message;
                    return View("SignIn");
                }
                ViewBag.Errors = result.Errors.ToList();
                return View();
            }
            ViewBag.Errors = validationResult.Errors[0];
            return View();
        }

        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAllAsync();
            return View(result.Payload);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserDto model)
        {
            var validator = new CreateUserValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                var result = await _userService.CreateAsync(model);
                if (result.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.AuthError = result.Payload;
                return View(model);
            }
            ViewBag.AuthError = validationResult.Errors[0];
            return View(model);
        }
        public async Task<IActionResult> Delete(string Id)
        {
            var user = await _userService.GetByIdAsync(Id);
            return View(user.Payload);
        }
        public async Task<IActionResult> DeleteById(string Id)
        {
            var result = await _userService.DeleteAsync(Id);
            if (result.Success)
            {
                return View(nameof(Index));
            }
            return View(nameof(Index));
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var result = await _userService.ConfirmEmailAsync(userId, token);
            if (result.Success)
            {
                ViewBag.AuthError = result.Message;
                return Redirect(nameof(SignIn));
            }
            return Redirect(nameof(SignIn));
        }

        public async Task<IActionResult> Edit(string id)
        {

            var result = await _userService.GetByIdAsync(id);
            await GetRoles();
            if (result.Success)
            {
                return View(result.Payload);
            }
            return View();
        }
        public async Task<IActionResult> EditUser(EditUserDto editUser)
        {
            EditUserValidation validation = new EditUserValidation();
            var validationResult = validation.Validate(editUser);
            if (validationResult.IsValid)
            {
                var result = await _userService.EditUserAsync(editUser);
                if (result.Success)
                {

                    return View("Index");
                }
                ViewBag.AuthError = result.Message;
                return View("Edit");
            }
            ViewBag.AuthError = validationResult.Errors[0];
            return View("Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePassword(ChangePasswordDto model)
        {
            var validator = new ChangePasswordValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                var result = await _userService.ChangePasswordAsync(model);
                return RedirectToAction(nameof(GetAll));
            }
            else
            {
                return View("Profile",validationResult.Errors);
            }
        }
        private async Task GetRoles()
        {
            var result = await _userService.LoadRoles();
            @ViewBag.RoleList = new SelectList(
          (System.Collections.IEnumerable)result, nameof(IdentityRole.Id),
              nameof(IdentityRole.Name)
              );
        }

    }
}
