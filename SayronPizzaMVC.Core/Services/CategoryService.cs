using Ardalis.Specification;
using AutoMapper;
using SayronPizzaMVC.Core.Entites.Category;
using SayronPizzaMVC.Core.Entites.Specification;
using SayronPizzaMVC.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SayronPizzaMVC.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<AppCategory> _categoryRepo;

        public CategoryService(IMapper mapper, IRepository<AppCategory> categoryRepo)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }
        public async Task<ServiceResponse> GetByName(string model)
        {
            var result = await _categoryRepo.GetItemBySpec(new CategorySpecification.GetByName(model));
            if (result != null)
            {
                return new ServiceResponse
                {
                    Success = true,
                    Message = "Category exists.",
                    Payload = result
                };
            }
            return new ServiceResponse
            {
                Success = true,
                Message = "Category successfully loaded.",
               
            };
        }

        public async Task<List<AppCategory>> GetAll()
        {
            var result = await _categoryRepo.GetAll();
            return _mapper.Map<List<AppCategory>>(result);
        }
        public async Task<AppCategory> Get(int id)
        {
            if (id < 0) return null;

            var category = await _categoryRepo.GetByID(id);
            if (category == null) return null;

            return _mapper.Map<AppCategory>(category);
        }
        public async Task Create(AppCategory model)
        {
            await _categoryRepo.Insert(_mapper.Map<AppCategory>(model));
            await _categoryRepo.Save();
        }
        public async Task Update(AppCategory model)
        {
            await _categoryRepo.Update(_mapper.Map<AppCategory>(model));
            await _categoryRepo.Save();
        }

        public async Task Delete(int id)
        {
            var result = await Get(id);
            if (result != null) return;
            await _categoryRepo.Delete(id);
            await _categoryRepo.Save();
        }

    }
}
