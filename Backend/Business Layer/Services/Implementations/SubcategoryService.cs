using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business_Layer.Services.Interfaces;
using Data_Layer.Models.CategoryModels;
using Data_Layer.Repositories.Interfaces;
using Data_Layer.UnitOfWork;

namespace Business_Layer.Services.Implementations
{
    public class SubcategoryService : BaseService<QuizSubcategory>
    {
        public SubcategoryService(IUnitOfWork unitOfWork) : base(unitOfWork.SubcategoryRepository, unitOfWork)
        {
        }

        public override async Task<bool> AddAsync(QuizSubcategory quizSubcategory)
        {
            var doesCategoryExist = await DoesCategoryExist(quizSubcategory.LanguageCategoryId);
            if (!doesCategoryExist) return false;
            var isUnique = await IsUnique(quizSubcategory.LanguageCategoryId, quizSubcategory.Name);
            if (!isUnique) return false;
            try
            {
                await _unitOfWork.SubcategoryRepository.AddAsync(quizSubcategory);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private async Task<bool> DoesCategoryExist(int categoryId)
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            return categories.Any(c => c.Id == categoryId);
        }

        private async Task<bool> IsUnique(int categoryId, string subcategoryName)
        {
            var entries = (await GetAllAsync()).Where(sc => sc.LanguageCategoryId == categoryId);
            return !entries.Any(sc => sc.Name.ToLower() == subcategoryName);
        }
    }
}
