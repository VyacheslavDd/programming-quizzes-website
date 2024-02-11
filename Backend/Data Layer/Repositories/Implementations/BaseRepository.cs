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

		public BaseRepository(DbSet<T> dbSet)
		{
			_dbSet = dbSet;
		}

		public virtual async Task AddAsync(T? item)
		{
			await _dbSet.AddAsync(item);
		}

		public virtual async Task DeleteAsync(int id)
		{
			var item = await GetByIdAsync(id);
			_dbSet.Remove(item);
		}

		public virtual async Task<List<T?>> GetAllAsync()
		{
			return await _dbSet.AsNoTracking().ToListAsync();
		}

		public virtual async Task<T?> GetByIdAsync(int id)
		{
			return await _dbSet.FindAsync(id);
		}
	}
}
