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
        Task<T?> GetByGuidAsync(Guid id);
        bool Add(T? item);
        Task<Guid> AddAsync(T? item);
        Task<bool> DeteteAsync(Guid id);
        Task<bool> UpdateAsync(T? item);
    }
}
