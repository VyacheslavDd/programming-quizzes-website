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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> AddCategory(LanguageCategory category)
        {
            var isUnique = await IsUnique(category.Name.ToLower());
            if (!isUnique) return false;
            try
            {
                await _repository.AddCategory(category);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<LanguageCategory?>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<LanguageCategory?> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<List<QuizSubcategory?>> GetSubcategories(int id)
        {
            return await _repository.GetSubcategories(id);
        }

        private async Task<bool> IsUnique(string categoryName)
        {
            var categories = await _repository.GetAll();
            return !categories.Any(c => c.Name.ToLower() == categoryName);
        }
    }
}
