using AutoMapper;
using SayronPizzaMVC.Core.DTO_s.Products;
using SayronPizzaMVC.Core.Entites.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SayronPizzaMVC.Core.AutoMappers.Product
{
    public class AutoMapperProductProfile : Profile
    {
        public AutoMapperProductProfile()
        {
            CreateMap<AppProduct, PizzaDto>();

            CreateMap<AppProduct, ProductDto>().ReverseMap();
        }
    }
}
