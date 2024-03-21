using Core.Base.Service.Implementations;
using ProgQuizWebsite.Domain.CategoryModels;
using ProgQuizWebsite.Infrastracture.UnitOfWork;
using ProgQuizWebsite.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProgQuizWebsite.Services.Implementations.MainServices
{
    public class SubcategoryService : BaseService<QuizSubcategory>
    {
        private readonly IValidationService _validationService;
        public SubcategoryService(IUnitOfWork unitOfWork, IValidationService validationService) : base(unitOfWork.SubcategoryRepository, unitOfWork)
        {
            _validationService = validationService;
        }

        public async override Task ValidateItemDataAsync(QuizSubcategory? quizSubcategory)
        {
            await _validationService.ValidateSubcategory(quizSubcategory, _unitOfWork.CategoryRepository, _repository);
        }
    }
}
