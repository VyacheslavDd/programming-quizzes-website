using Business_Layer.Services.Interfaces;
using Data_Layer.Models.CategoryModels;
using Data_Layer.Models.QuizContentModels;
using Data_Layer.Repositories.Interfaces;
using Data_Layer.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.Implementations
{
	public abstract class BaseService<T> : IService<T> where T : class
	{
		protected readonly IUnitOfWork _unitOfWork;
		protected readonly IRepository<T> _repository;

		public BaseService(IRepository<T> repository, IUnitOfWork unitOfWork)
		{
			_repository = repository;
			_unitOfWork = unitOfWork;
		}

		public abstract Task<bool> ValidateItemData(T? item);

		public async virtual Task<bool> AddAsync(T? item)
		{
			if (! await ValidateItemData(item)) return false;
			try
			{
				await _repository.AddAsync(item);
				await _unitOfWork.Save();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public virtual async Task<bool> DeteteAsync(int id)
		{
			try
			{
				await _repository.DeleteAsync(id);
				await _unitOfWork.Save();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public virtual async Task<List<T?>> GetAllAsync()
		{
			return await _repository.GetAllAsync();
		}

		public virtual async Task<T?> GetByIdAsync(int id)
		{
			return await _repository.GetByIdAsync(id);
		}

		public virtual async Task<bool> UpdateAsync(T? item)
		{
			if (item is null) return false;
			if (!await ValidateItemData(item)) return false;
			try
			{
				await _unitOfWork.Save();
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
