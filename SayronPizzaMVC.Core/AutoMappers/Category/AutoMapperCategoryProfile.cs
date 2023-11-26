using AutoMapper;
using SayronPizzaMVC.Core.DTO_s.Categories;
using SayronPizzaMVC.Core.DTO_s.Products;
using SayronPizzaMVC.Core.Entites.Category;
using SayronPizzaMVC.Core.Entites.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SayronPizzaMVC.Core.AutoMappers.Category
{
    public class AutoMapperCategoryProfile : Profile
    {
        public AutoMapperCategoryProfile()
        {
            CreateMap<AppCategory, CategoryDto>().ReverseMap();
        }
    }
}
