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

namespace Business_Layer.Services.Implementations
{
    public class CategoryService : BaseService<LanguageCategory>
    {
        public CategoryService(IUnitOfWork unitOfWork) : base(unitOfWork.CategoryRepository, unitOfWork)
        {
        }

		public async override Task<bool> ValidateItemData(LanguageCategory? category)
		{
			var isUnique = await IsUnique(category.Name.ToLower());
			if (!isUnique) return false;
            return true;
		}

		private async Task<bool> IsUnique(string categoryName)
        {
            var categories = await GetAllAsync();
            return !categories.Any(c => c.Name.ToLower() == categoryName);
        }
    }
}
