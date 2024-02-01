using Data_Layer.Models.CategoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<LanguageCategory?>> GetAll();
        Task<LanguageCategory?> GetById(int id);
        Task AddCategory(LanguageCategory category);
    }
}
