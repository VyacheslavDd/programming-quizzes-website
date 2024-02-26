using Data_Layer.Contexts;
using Data_Layer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Repositories.Implementations
{
	public abstract class BaseRepository<T> : IRepository<T> where T : class
	{
		protected readonly DbSet<T> _dbSet;
		protected readonly QuizAppContext _context;

		public BaseRepository(DbSet<T> dbSet)
		{
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
	}
}
