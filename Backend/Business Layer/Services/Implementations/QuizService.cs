using Business_Layer.Services.Interfaces;
using Data_Layer.FilterModels.QuizFilters;
using Data_Layer.Models.CategoryModels;
using Data_Layer.Models.QuizModels;
using Data_Layer.Repositories.Interfaces;
using Data_Layer.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.Implementations
{
    public class QuizService : BaseService<Quiz>, IQuizService
    {

        public QuizService(IUnitOfWork unitOfWork) : base(unitOfWork.QuizRepository, unitOfWork)
        {
        }

        public override async Task<bool> AddAsync(Quiz quiz)
        {
            return true;
        }

        public async override Task<bool> ValidateItemData(Quiz? quiz)
        {
            return true;
        }

        public async Task<bool> AddAsync(Quiz quiz, List<int> subcategoriesId)
        {
            var doesCategoryExist = await DoesCategoryExist(quiz.LanguageCategoryId);
            if (!doesCategoryExist) return false;
            var matchSubcategories = await MatchSubcategories(quiz, subcategoriesId);
            if (!matchSubcategories) return false;
            var isUnique = await IsUnique(quiz.LanguageCategoryId, quiz.Title.ToLower());
            if (!isUnique) return false;
            return await base.AddAsync(quiz);
        }

        private async Task<bool> DoesCategoryExist(int categoryId)
        {
            return (await _unitOfWork.CategoryRepository.GetByIdAsync(categoryId)) is not null;
        }

        public async Task<bool> MatchSubcategories(Quiz quiz, List<int> subcategoriesId)
        {
            if (quiz is null) return false;
            List<QuizSubcategory> subcategories = new List<QuizSubcategory>();
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(quiz.LanguageCategoryId);
            if (category is null) return false;
			foreach (var subcategoryId in subcategoriesId)
            {
                var subcategory = await _unitOfWork.SubcategoryRepository.GetByIdAsync(subcategoryId);
                if (subcategory is null) return false;
                if (!category.Subcategories.Any(sc => sc.Id == subcategoryId)) return false;
                subcategories.Add(subcategory);
            }
            foreach (var subcategory in subcategories)
            {
                subcategory.Quizzes.Add(quiz);
                quiz.Subcategories.Add(subcategory);
            }
            return true;
        }

        private async Task<bool> IsUnique(int categoryId, string title)
        {
            var quizzes = (await _unitOfWork.QuizRepository.GetAllAsync()).Where(qz => qz.LanguageCategoryId == categoryId);
            return !quizzes.Any(qz => qz.Title.ToLower() == title);
        }

		public async Task<List<Quiz?>> GetByPageFilter(GetQuizzesFilter filter)
		{
            if (filter is null || filter.Page - 1 < 0) return new List<Quiz?>();
            return (await GetAllAsync())
                .Skip((filter.Page - 1) * filter.Limit).Take(filter.Limit).ToList();
		}
	}
}
