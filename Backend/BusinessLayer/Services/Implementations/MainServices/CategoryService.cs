using AutoMapper;
using Business_Layer.Services.Interfaces;
using Data_Layer.Models.CategoryModels;
using Data_Layer.Repositories.Interfaces;
using Data_Layer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.Implementations.MainServices
{
    public class CategoryService : BaseService<LanguageCategory>, ICategoryService
    {
        private readonly IValidationService _validationService;
        public CategoryService(IUnitOfWork unitOfWork, IValidationService validationService) : base(unitOfWork.CategoryRepository, unitOfWork)
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

		public async override Task ValidateItemData(LanguageCategory? category)
        {
            await _validationService.ValidateCategory(category, _repository);
        }
    }
}
