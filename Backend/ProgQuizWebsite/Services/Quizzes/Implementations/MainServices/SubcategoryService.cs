using Core.Base.Service.Implementations;
using Core.Redis.Interfaces;
using ProgQuizWebsite.Domain.Quizzes.Models.CategoryModels;
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
    public class SubcategoryService : BaseService<QuizSubcategory>
    {
        private readonly IValidationService _validationService;
        public SubcategoryService(IUnitOfWork unitOfWork, IValidationService validationService, IRedisService redisService, QuizAppContext quizAppContext)
            : base(unitOfWork.SubcategoryRepository, unitOfWork, redisService, quizAppContext)
        {
            _validationService = validationService;
        }

        public async override Task ValidateItemDataAsync(QuizSubcategory? quizSubcategory)
        {
            await _validationService.ValidateSubcategory(quizSubcategory, _unitOfWork.CategoryRepository, _repository);
        }
    }
}
