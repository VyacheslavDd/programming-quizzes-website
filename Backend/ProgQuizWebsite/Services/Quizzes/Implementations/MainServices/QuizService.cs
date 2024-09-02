
using Core.Base.Service.Implementations;
using Core.Base.Service.Interfaces;
using Core.Constants;
using Core.Redis.Interfaces;
using Microsoft.EntityFrameworkCore;
using ProgQuizWebsite.Domain.Quizzes.FilterModels;
using ProgQuizWebsite.Domain.Quizzes.Models.CategoryModels;
using ProgQuizWebsite.Domain.Quizzes.Models.QuizModels;
using ProgQuizWebsite.Infrastracture.Contexts;
using ProgQuizWebsite.Infrastracture.Quizzes.UnitOfWork;
using ProgQuizWebsite.Services.Quizzes.Implementations;
using ProgQuizWebsite.Services.Quizzes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgQuizWebsite.Services.Quizzes.Implementations.MainServices
{
    public class QuizService : BaseService<Quiz>, IQuizService
    {
        private readonly IValidationService _validationService;
        public QuizService(IUnitOfWork unitOfWork, IValidationService validationService, IRedisService redisService, QuizAppContext quizAppContext)
            : base(unitOfWork.QuizRepository, unitOfWork, redisService, quizAppContext)
        {
            _validationService = validationService;
        }

        public override async Task<Guid> AddAsync(Quiz quiz)
        {
            return new Guid();
        }

        public async override Task ValidateItemDataAsync(Quiz? quiz)
        {
        }

        public async Task<Guid> AddAsync(Quiz quiz, List<Guid> subcategoriesId)
        {
            await _validationService.ValidateQuiz(quiz, _unitOfWork.CategoryRepository, _repository);
            await MatchSubcategoriesAsync(quiz, subcategoriesId);
            return await base.AddAsync(quiz);
        }

        public async Task MatchSubcategoriesAsync(Quiz quiz, List<Guid> subcategoriesId)
        {
            if (quiz is null) throw new ArgumentNullException("Викторины не существует!");
            List<QuizSubcategory> subcategories = new List<QuizSubcategory>();
            var category = await _unitOfWork.CategoryRepository.GetByGuidAsync(quiz.LanguageCategoryId);
            foreach (var subcategoryId in subcategoriesId)
            {
                var subcategory = await _unitOfWork.SubcategoryRepository.GetByGuidAsync(subcategoryId);
                if (subcategory is null) throw new ArgumentNullException("Одна из указанных подкатегорий не существует!");
                if (!category.Subcategories.Any(sc => sc.Id == subcategoryId))
                    throw new ArgumentNullException("Одна из указанных подкатегорий не входит в состав указанной категории!");
                subcategories.Add(subcategory);
            }
            foreach (var subcategory in subcategories)
            {
                subcategory.Quizzes.Add(quiz);
                quiz.Subcategories.Add(subcategory);
            }
        }

        public async Task<List<Quiz?>> GetByFilterAsync(GetQuizzesFilter filter, HttpResponse response, CancellationToken cancellationToken = default)
        {
            if (filter is null || filter.Page - 1 < 0 || filter.Limit <= 0) return new List<Quiz?>();
            var quizzes = (await GetAllAsync(cancellationToken))
                .Where(q => filter.CategoryId == Guid.Empty || q.LanguageCategoryId == filter.CategoryId)
                .Where(q => filter.SubcategoryId == Guid.Empty || q.Subcategories.Any(s => s.Id == filter.SubcategoryId))
                .Where(q => filter.Difficulty <= 0 || q.Difficulty == filter.Difficulty)
                .OrderByDescending(q => q.CreationDate);
            response.Headers.Add(SpecialConstants.ContentCountHeaderName, quizzes.Count().ToString());
            return quizzes.Skip((filter.Page - 1) * filter.Limit).Take(filter.Limit).ToList();
        }
    }
}
