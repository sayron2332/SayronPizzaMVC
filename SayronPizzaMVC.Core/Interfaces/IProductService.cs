﻿using SayronPizzaMVC.Core.DTO_s.Products;
using SayronPizzaMVC.Core.Entites.Category;
using SayronPizzaMVC.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SayronPizzaMVC.Core.Interfaces
{
    internal interface IProductService
    {
        public Task<List<PizzaDto>> GetAllPizza();
        public Task<List<ProductDto>> GetAllProducts();
        public Task<ProductDto> GetById(int Id);
        public Task Update(ProductDto product);

    }
}
