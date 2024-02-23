using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.Interfaces
{
    public interface IService<T> where T: class
    {
        Task<List<T?>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        bool Add(T? item);
        Task<bool> AddAsync(T? item);
        Task<bool> DeteteAsync(int id);
        Task<bool> UpdateAsync(T? item);
    }
}
