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
    public class SubcategoryService : ISubcategoryService
    {
        private readonly ISubcategoryRepository _repository;
        private readonly ICategoryRepository _categoryRepository;

        public SubcategoryService(ISubcategoryRepository repository, ICategoryRepository categoryRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> AddSubcategory(QuizSubcategory quizSubcategory)
        {
            var doesCategoryExist = await DoesCategoryExist(quizSubcategory.LanguageCategoryId);
            if (!doesCategoryExist) return false;
            var isUnique = await IsUnique(quizSubcategory.LanguageCategoryId, quizSubcategory.Name);
            if (!isUnique) return false;
            try
            {
                await _repository.AddSubcategory(quizSubcategory);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<QuizSubcategory?>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<QuizSubcategory?> GetSubcategoryById(int id)
        {
            return await _repository.GetSubcategoryById(id);
        }

        private async Task<bool> DoesCategoryExist(int categoryId)
        {
            var categories = await _categoryRepository.GetAll();
            return categories.Any(c => c.Id == categoryId);
        }

        private async Task<bool> IsUnique(int categoryId, string subcategoryName)
        {
            var entries = (await GetAll()).Where(sc => sc.LanguageCategoryId == categoryId);
            return !entries.Any(sc => sc.Name.ToLower() == subcategoryName);
        }
    }
}
