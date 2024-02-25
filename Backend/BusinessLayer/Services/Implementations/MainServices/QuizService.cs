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

namespace Business_Layer.Services.Implementations.MainServices
{
    public class QuizService : BaseService<Quiz>, IQuizService
    {

        public QuizService(IUnitOfWork unitOfWork) : base(unitOfWork.QuizRepository, unitOfWork)
        {
        }

        public override async Task<Guid> AddAsync(Quiz quiz)
        {
            return new Guid();
        }

        public async override Task<bool> ValidateItemData(Quiz? quiz)
        {
            return true;
        }

        public async Task<Guid> AddAsync(Quiz quiz, List<Guid> subcategoriesId)
        {
            var doesCategoryExist = await DoesCategoryExist(quiz.LanguageCategoryId);
            if (!doesCategoryExist) return Guid.Empty;
            var matchSubcategories = await MatchSubcategories(quiz, subcategoriesId);
            if (!matchSubcategories) return Guid.Empty;
            var isUnique = await IsUnique(quiz.LanguageCategoryId, quiz.Title.ToLower());
            if (!isUnique) return Guid.Empty;
            return await base.AddAsync(quiz);
        }

        private async Task<bool> DoesCategoryExist(Guid categoryId)
        {
            return await _unitOfWork.CategoryRepository.GetByGuidAsync(categoryId) is not null;
        }

        public async Task<bool> MatchSubcategories(Quiz quiz, List<Guid> subcategoriesId)
        {
            if (quiz is null) return false;
            List<QuizSubcategory> subcategories = new List<QuizSubcategory>();
            var category = await _unitOfWork.CategoryRepository.GetByGuidAsync(quiz.LanguageCategoryId);
            if (category is null) return false;
            foreach (var subcategoryId in subcategoriesId)
            {
                var subcategory = await _unitOfWork.SubcategoryRepository.GetByGuidAsync(subcategoryId);
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

        private async Task<bool> IsUnique(Guid categoryId, string title)
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
