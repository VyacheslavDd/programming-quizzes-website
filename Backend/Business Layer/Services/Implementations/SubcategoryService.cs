using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business_Layer.Services.Interfaces;
using Data_Layer.Models.CategoryModels;
using Data_Layer.Repositories.Interfaces;

namespace Business_Layer.Services.Implementations
{
    public class SubcategoryService : IService<QuizSubcategory>
    {
        private readonly IRepository<QuizSubcategory> _repository;
        private readonly IRepository<LanguageCategory> _categoryRepository;

        public SubcategoryService(IRepository<QuizSubcategory> repository, IRepository<LanguageCategory> categoryRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> AddAsync(QuizSubcategory quizSubcategory)
        {
            var doesCategoryExist = await DoesCategoryExist(quizSubcategory.LanguageCategoryId);
            if (!doesCategoryExist) return false;
            var isUnique = await IsUnique(quizSubcategory.LanguageCategoryId, quizSubcategory.Name);
            if (!isUnique) return false;
            try
            {
                await _repository.AddAsync(quizSubcategory);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<QuizSubcategory?>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<QuizSubcategory?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        private async Task<bool> DoesCategoryExist(int categoryId)
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.Any(c => c.Id == categoryId);
        }

        private async Task<bool> IsUnique(int categoryId, string subcategoryName)
        {
            var entries = (await GetAllAsync()).Where(sc => sc.LanguageCategoryId == categoryId);
            return !entries.Any(sc => sc.Name.ToLower() == subcategoryName);
        }
    }
}
