using FluentValidation;
using SayronPizzaMVC.Core.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SayronPizzaMVC.Core.Validation.User
{
    public class EditUserValidation : AbstractValidator<EditUserDto>
    {
        public EditUserValidation()
        {
            RuleFor(r => r.FirstName).NotEmpty().MaximumLength(64).MinimumLength(2);
            RuleFor(r => r.LastName).NotEmpty().MaximumLength(64).MinimumLength(2);
            RuleFor(r => r.Email).NotEmpty().EmailAddress().MaximumLength(128);
            RuleFor(r => r.Role).NotEmpty();
        }
    }
}
