using Ardalis.Specification;
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
        Task<List<AppCategory>> GetAll();
        Task<AppCategory> Get(int id);
        Task<ServiceResponse> GetByName(AppCategory model);
        Task Create(AppCategory model);
        Task Update(AppCategory model);
        Task Delete(int id);
    }
}
