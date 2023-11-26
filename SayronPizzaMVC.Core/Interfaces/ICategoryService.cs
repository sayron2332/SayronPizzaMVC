using Ardalis.Specification;
using SayronPizzaMVC.Core.DTO_s.Categories;
using SayronPizzaMVC.Core.Entites.Category;
using SayronPizzaMVC.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SayronPizzaMVC.Core.Interfaces
{
    internal interface ICategoryService
    {
        Task<List<CategoryDto>> GetAll();
        Task<CategoryDto> Get(int id);
        Task<ServiceResponse> GetByName(CategoryDto model);
        Task Create(CategoryDto model);
        Task Update(CategoryDto model);
        Task Delete(int id);
    }
}
