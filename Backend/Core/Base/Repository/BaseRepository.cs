
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Base.Repository
{
	public abstract class BaseRepository<T> : IRepository<T> where T : class
	{
		protected readonly DbSet<T> _dbSet;
		protected readonly DbContext _context;

		public BaseRepository(DbContext context, DbSet<T> dbSet)
		{
			_context = context;
			_dbSet = dbSet;
		}

		public virtual async Task<Guid> AddAsync(T? item)
		{
			var result = await _dbSet.AddAsync(item);
			return (Guid)result.Property("Id").CurrentValue;
		}

		public virtual async Task DeleteAsync(Guid id)
		{
			var item = await GetByGuidAsync(id);
			_dbSet.Remove(item);
		}

		public virtual async Task<List<T?>> GetAllAsync()
		{
			return await _dbSet.AsNoTracking().ToListAsync();
		}

		public virtual async Task<T?> GetByGuidAsync(Guid id)
		{
			return await _dbSet.FindAsync(id);
		}

		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}
