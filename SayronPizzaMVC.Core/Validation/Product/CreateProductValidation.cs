using FluentValidation;
using SayronPizzaMVC.Core.DTO_s;
using SayronPizzaMVC.Core.DTO_s.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SayronPizzaMVC.Core.Validation.Product
{
    public class CreateProductValidation : AbstractValidator<ProductDto>
    {
        public CreateProductValidation()
        {
            RuleFor(r => r.Name).NotEmpty().MinimumLength(3).WithMessage("Min length 3");
            RuleFor(r => r.Description).NotEmpty();
            RuleFor(r => r.Price).NotEmpty();
            RuleFor(r => r.Size).NotEmpty();
        }
    }
}
