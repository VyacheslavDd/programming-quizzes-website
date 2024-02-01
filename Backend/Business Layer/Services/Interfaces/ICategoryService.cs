using Data_Layer.Models.CategoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<LanguageCategory?>> GetAll();
        Task<LanguageCategory?> GetById(int id);
        Task<bool> AddCategory(LanguageCategory category);
    }
}
