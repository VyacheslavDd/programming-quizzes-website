using Core.Base.Service.Implementations;
using Core.Base.Service.Interfaces;
using Core.Redis.Implementations;
using Core.Redis.Interfaces;
using ProgQuizWebsite.Domain.CategoryModels;
using ProgQuizWebsite.Domain.QuizContentModels;
using ProgQuizWebsite.Domain.QuizModels;
using ProgQuizWebsite.Services.Implementations.AdditionalServices;
using ProgQuizWebsite.Services.Implementations.MainServices;
using ProgQuizWebsite.Services.Interfaces;

namespace ProgQuizWebsite.Infrastracture.Startups
{
	public static class ServicesStartup
	{
		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			services.AddScoped<IValidationService, ValidationService>();
			services.AddScoped<IImageService, ImageService>();
			services.AddScoped<IService<QuizSubcategory>, SubcategoryService>();
			services.AddScoped<IQuizService, QuizService>();
			services.AddScoped<ICategoryService, CategoryService>();
			services.AddScoped<IService<Question>, QuestionService>();
			services.AddScoped<IService<Answer>, AnswerService>();
			return services;
		}
	}
}
