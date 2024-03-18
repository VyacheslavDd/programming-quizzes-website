using Data_Layer.Models.CategoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.Interfaces
{
	public interface ICategoryService : IService<LanguageCategory>
	{
		Task<List<QuizSubcategory>> GetConnectedSubcategoriesAsync(Guid categoryId);
	}
}
