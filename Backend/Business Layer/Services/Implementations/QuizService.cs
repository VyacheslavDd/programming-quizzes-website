using Business_Layer.Services.Interfaces;
using Data_Layer.Models.CategoryModels;
using Data_Layer.Models.QuizModels;
using Data_Layer.Repositories.Interfaces;
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
        private readonly IQuizRepository _quizRepository;
        private readonly ISubcategoryService _subcategoryService;
        private readonly ICategoryService _categoryService;

        public QuizService(IQuizRepository repository, ICategoryService categoryService, ISubcategoryService subcategoryService)
        {
            _quizRepository = repository;
            _categoryService = categoryService;
            _subcategoryService = subcategoryService;
        }

        public async Task<bool> AddQuiz(Quiz quiz, List<int> subcategoriesId)
        {
            var doesCategoryExist = await DoesCategoryExist(quiz.LanguageCategoryId);
            if (!doesCategoryExist) return false;
            var matchSubcategories = await MatchSubcategories(quiz, subcategoriesId);
            if (!matchSubcategories) return false;
            var isUnique = await IsUnique(quiz.LanguageCategoryId, quiz.Title.ToLower());
            if (!isUnique) return false;
            try
            {
                await _quizRepository.AddQuiz(quiz);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Quiz>> GetAll()
        {
            return await _quizRepository.GetAll();
        }

        public async Task<Quiz?> GetById(int id)
        {
            return await _quizRepository.GetById(id);
        }

        private async Task<bool> DoesCategoryExist(int categoryId)
        {
            return (await _categoryService.GetById(categoryId)) is not null;
        }

        private async Task<bool> MatchSubcategories(Quiz quiz, List<int> subcategoriesId)
        {
            List<QuizSubcategory> subcategories = new List<QuizSubcategory>();
            foreach (var subcategoryId in subcategoriesId)
            {
                var subcategory = await _subcategoryService.GetSubcategoryById(subcategoryId);
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
            var quizzes = (await _quizRepository.GetAll()).Where(qz => qz.LanguageCategoryId == categoryId);
            return !quizzes.Any(qz => qz.Title.ToLower() == title);
        }
    }
}
