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
        private readonly IValidationService _validationService;
        public QuizService(IUnitOfWork unitOfWork, IValidationService validationService) : base(unitOfWork.QuizRepository, unitOfWork)
        {
            _validationService = validationService;
        }

        public override async Task<Guid> AddAsync(Quiz quiz)
        {
            return new Guid();
        }

        public async override Task ValidateItemData(Quiz? quiz)
        {
        }

        public async Task<Guid> AddAsync(Quiz quiz, List<Guid> subcategoriesId)
        {
            await _validationService.ValidateQuiz(quiz, _unitOfWork.CategoryRepository, _repository);
			await MatchSubcategories(quiz, subcategoriesId);
            return await base.AddAsync(quiz);
        }

        public async Task MatchSubcategories(Quiz quiz, List<Guid> subcategoriesId)
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

        public async Task<List<Quiz?>> GetByPageFilter(GetQuizzesFilter filter)
        {
            if (filter is null || filter.Page - 1 < 0) return new List<Quiz?>();
            return (await GetAllAsync())
                .Skip((filter.Page - 1) * filter.Limit).Take(filter.Limit).ToList();
        }
    }
}
