using AutoMapper;
using SayronPizzaMVC.Core.DTO_s.Products;
using SayronPizzaMVC.Core.Entites.Category;
using SayronPizzaMVC.Core.Entites.Product;
using SayronPizzaMVC.Core.Entites.Specification;
using SayronPizzaMVC.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SayronPizzaMVC.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<AppProduct> _productRepo;
        private readonly CategoryService _categoryService;
        private readonly IMapper _mapper;
        public ProductService(IRepository<AppProduct> productRepo, CategoryService categoryService, IMapper mapper)
        {
            _productRepo = productRepo;
            _categoryService = categoryService;
            _mapper = mapper;

        }

        public async Task<ServiceResponse> GetAllPizza()
        {
            var category = await _categoryService.GetByName("Pizza");
            if (category.Payload == null)
            {
                return new ServiceResponse 
                {
                    Success = false,
                    Message = "We dont have this category"
                };

            }
            var result = await _productRepo.GetListBySpec(new ProductSpecification.GetAllByCategory((AppCategory)category.Payload));
            List<PizzaDto> pizzaList = _mapper.Map<List<PizzaDto>>(result);

            if (result == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "We dont have products for this category"
                };

            }
            return new ServiceResponse
            {
                Success = true,
                Message = "all ok",
                Payload = pizzaList
            };


        }

        public async Task<ServiceResponse> GetAllProducts()
        {
            var result = await _productRepo.GetAll();
            List<ProductDto> productsList = _mapper.Map<List<ProductDto>>(result);
            
            return new ServiceResponse { Success = true, Payload = productsList };
        }
    }
}
