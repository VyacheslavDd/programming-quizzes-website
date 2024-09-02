using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Base.Service.Interfaces
{
    public interface IService<T> where T : class
    {
        Task<List<T?>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<T?> GetByGuidAsync(Guid id);
        Task<Guid> AddAsync(T? item);
        Task DeteteAsync(Guid id);
        Task UpdateAsync(T? item);
    }
}
