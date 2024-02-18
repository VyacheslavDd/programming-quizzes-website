using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Repositories.Interfaces
{
    public interface IRepository<T> where T: class
    {
        Task<List<T?>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T? item);
        Task DeleteAsync(int id);
    }
}
