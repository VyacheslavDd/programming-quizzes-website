using AutoMapper;
using Business_Layer.Services.Interfaces;
using Data_Layer.Models.CategoryModels;
using Data_Layer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.Implementations
{
    public class CategoryService : IService<LanguageCategory>
    {
        private readonly IRepository<LanguageCategory> _repository;

        public CategoryService(IRepository<LanguageCategory> repository)
        {
            _repository = repository;
        }

        public async Task<bool> AddAsync(LanguageCategory category)
        {
            var isUnique = await IsUnique(category.Name.ToLower());
            if (!isUnique) return false;
            try
            {
                await _repository.AddAsync(category);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<LanguageCategory?>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<LanguageCategory?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        private async Task<bool> IsUnique(string categoryName)
        {
            var categories = await _repository.GetAllAsync();
            return !categories.Any(c => c.Name.ToLower() == categoryName);
        }
    }
}
