using FluentValidation;
using SayronPizzaMVC.Core.DTO_s.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SayronPizzaMVC.Core.Validation.Category
{
    public class CreateCategoryValidation : AbstractValidator<CategoryDto>
    {
        public CreateCategoryValidation()
        {
            RuleFor(r => r.Name).NotEmpty().MinimumLength(3).WithMessage("Min length 3");
         
        }
    }
}
