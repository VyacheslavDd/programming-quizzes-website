using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business_Layer.Services.Interfaces;
using Data_Layer.Models.CategoryModels;
using Data_Layer.Repositories.Interfaces;
using Data_Layer.UnitOfWork;

namespace Business_Layer.Services.Implementations.MainServices
{
    public class SubcategoryService : BaseService<QuizSubcategory>
    {
        private readonly IValidationService _validationService;
        public SubcategoryService(IUnitOfWork unitOfWork, IValidationService validationService) : base(unitOfWork.SubcategoryRepository, unitOfWork)
        {
            _validationService = validationService;
        }

        public async override Task ValidateItemData(QuizSubcategory? quizSubcategory)
        {
            await _validationService.ValidateSubcategory(quizSubcategory, _unitOfWork.CategoryRepository, _repository);
        }
    }
}
