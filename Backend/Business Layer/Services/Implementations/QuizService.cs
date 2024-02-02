using Business_Layer.Services.Interfaces;
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
    public class QuizService : IQuizService
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuizService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddAsync(Quiz quiz)
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
            try
            {
                await _unitOfWork.QuizRepository.AddAsync(quiz);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Quiz>> GetAllAsync()
        {
            return await _unitOfWork.QuizRepository.GetAllAsync();
        }

        public async Task<Quiz?> GetByIdAsync(int id)
        {
            return await _unitOfWork.QuizRepository.GetByIdAsync(id);
        }

        private async Task<bool> DoesCategoryExist(int categoryId)
        {
            return (await _unitOfWork.CategoryRepository.GetByIdAsync(categoryId)) is not null;
        }

        private async Task<bool> MatchSubcategories(Quiz quiz, List<int> subcategoriesId)
        {
            List<QuizSubcategory> subcategories = new List<QuizSubcategory>();
            foreach (var subcategoryId in subcategoriesId)
            {
                var subcategory = await _unitOfWork.SubcategoryRepository.GetByIdAsync(subcategoryId);
                if (subcategory is null) return false;
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
    }
}
