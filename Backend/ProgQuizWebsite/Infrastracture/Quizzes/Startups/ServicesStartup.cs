using Core.Base.Service.Implementations;
using Core.Base.Service.Interfaces;
using Core.Redis.Implementations;
using Core.Redis.Interfaces;
using ProgQuizWebsite.Domain.Quizzes.Models.QuizModels;
using ProgQuizWebsite.Domain.Quizzes.Models.CategoryModels;
using ProgQuizWebsite.Domain.Quizzes.Models.QuizContentModels;
using ProgQuizWebsite.Services.Quizzes.Implementations.AdditionalServices;
using ProgQuizWebsite.Services.Quizzes.Implementations.MainServices;
using ProgQuizWebsite.Services.Quizzes.Interfaces;
using ProgQuizWebsite.Infrastracture.Quizzes.Filters;

namespace ProgQuizWebsite.Infrastracture.Quizzes.Startups
{
    public static class ServicesStartup
    {
        public static IServiceCollection AddQuizServices(this IServiceCollection services)
        {
            services.AddScoped<IValidationService, ValidationService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IService<QuizSubcategory>, SubcategoryService>();
            services.AddScoped<IQuizService, QuizService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IQuizRatingService, QuizRatingService>();
            services.AddScoped<IService<Question>, QuestionService>();
            services.AddScoped<IService<Answer>, AnswerService>();
			services.AddScoped<QuizElementsExceptionFilter>();
			return services;
        }
    }
}
