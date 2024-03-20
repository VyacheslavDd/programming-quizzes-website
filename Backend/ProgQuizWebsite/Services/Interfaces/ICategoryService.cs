
using Core.Base.Service.Interfaces;
using ProgQuizWebsite.Domain.CategoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgQuizWebsite.Services.Interfaces
{
	public interface ICategoryService : IService<LanguageCategory>
	{
		Task<List<QuizSubcategory>> GetConnectedSubcategoriesAsync(Guid categoryId);
	}
}
