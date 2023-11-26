using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
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
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public ProductService(IRepository<AppProduct> productRepo, IMapper mapper, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _productRepo = productRepo;
         
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }

        public async Task<List<PizzaDto>> GetAllPizza()
        {
            var result = await _productRepo.GetListBySpec(new ProductSpecification.GetAllByCategoryName("pizza"));
            List<PizzaDto> pizzaList = _mapper.Map<List<PizzaDto>>(result);
            return pizzaList;
        }

        public async Task<List<ProductDto>> GetAllProducts()
        {
            var result = await _productRepo.GetListBySpec(new ProductSpecification.All());;
            return _mapper.Map<List<ProductDto>>(result);
        }

        public async Task Create(ProductDto model)
        {
            if (model.File.Count > 0)
            {
                string webRootPath = _webHostEnvironment.WebRootPath;
                string upload = webRootPath + _configuration.GetValue<string>("ImageSettings:ImagePath");
                var files = model.File;
                string fileName = Guid.NewGuid().ToString();
                string extensions = Path.GetExtension(files[0].FileName);
                using (var fileStream = new FileStream(Path.Combine(upload, fileName + extensions), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                model.ImagePath = fileName + extensions;
            }
            else
            {
                model.ImagePath = "Default.png";
            }
            var product = _mapper.Map<AppProduct>(model);
            product.AppCategory = null;
            await _productRepo.Insert(product);
            await _productRepo.Save();


        }

        public async Task<ProductDto> GetById(int Id)
        {
            AppProduct product = await _productRepo.GetByID(Id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task Update(ProductDto product)
        {
            var currentProduct = await _productRepo.GetByID(product.Id);
            if (product.File.Count > 0)
            {
                string webPathRoot = _webHostEnvironment.WebRootPath;
                string upload = webPathRoot + _configuration.GetValue<string>("ImageSettings:ImagePath");

                string existingFilePath = Path.Combine(upload, currentProduct.ImagePath);

                if (File.Exists(existingFilePath) && product.ImagePath != "Default.png")
                {
                    File.Delete(existingFilePath);
                }

                var files = product.File;

                string fileName = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(files[0].FileName);
                using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                product.ImagePath = fileName + extension;

            }
            else
            {
                product.ImagePath = currentProduct.ImagePath;
            }
            AppProduct updateProduct = _mapper.Map<AppProduct>(product);
            updateProduct.AppCategory = null;
            await _productRepo.Update(updateProduct);
            await _productRepo.Save();

        }
    }
    
}
