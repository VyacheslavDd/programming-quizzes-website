using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Base.Repository
{
    public interface IRepository<T> where T: class
    {
        Task<List<T?>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<T?> GetByGuidAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Guid> AddAsync(T? item);
        Task DeleteAsync(Guid id);
        Task SaveChangesAsync();
    }
}
