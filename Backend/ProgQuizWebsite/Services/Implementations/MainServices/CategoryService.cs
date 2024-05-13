using AutoMapper;
using Core.Base.Service.Implementations;
using Core.Redis.Interfaces;
using ProgQuizWebsite.Domain.CategoryModels;
using ProgQuizWebsite.Infrastracture.Contexts;
using ProgQuizWebsite.Infrastracture.UnitOfWork;
using ProgQuizWebsite.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ProgQuizWebsite.Services.Implementations.MainServices
{
    public class CategoryService : BaseService<LanguageCategory>, ICategoryService
    {
        private readonly IValidationService _validationService;
        public CategoryService(IUnitOfWork unitOfWork, IValidationService validationService, IRedisService redisService, QuizAppContext quizAppContext)
            : base(unitOfWork.CategoryRepository, unitOfWork, redisService, quizAppContext)
        {
            _validationService = validationService;
        }

		public async Task<List<QuizSubcategory>> GetConnectedSubcategoriesAsync(Guid categoryId)
		{
            var category = await _unitOfWork.CategoryRepository.GetByGuidAsync(categoryId);
            if (category is null) throw new ArgumentNullException("Укажите существующую категорию!");
            var subcategories = (await _unitOfWork.SubcategoryRepository.GetAllAsync())
                .Where(s => s.LanguageCategoryId == categoryId).ToList();
            return subcategories;
		}

		public async override Task ValidateItemDataAsync(LanguageCategory? category)
        {
            await _validationService.ValidateCategory(category, _repository);
        }
    }
}
