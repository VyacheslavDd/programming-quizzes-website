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

namespace Business_Layer.Services.Implementations.MainServices
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

        public async virtual Task<Guid> AddAsync(T? item)
        {
            if (!await ValidateItemData(item)) return Guid.Empty;
            try
            {
                var guid = await _repository.AddAsync(item);
                await _unitOfWork.SaveAsync();
                return guid;
            }
            catch
            {
                return Guid.Empty;
            }
        }

        public virtual async Task<bool> DeteteAsync(Guid id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                await _unitOfWork.SaveAsync();
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

        public virtual async Task<T?> GetByGuidAsync(Guid id)
        {
            return await _repository.GetByGuidAsync(id);
        }

        public virtual async Task<bool> UpdateAsync(T? item)
        {
            if (item is null) return false;
            if (!await ValidateItemData(item)) return false;
            try
            {
                await _unitOfWork.SaveAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

		public bool Add(T? item)
		{
            var isCorrect = ValidateItemData(item).Result;
            if (!isCorrect) return false;
            try
            {
                _repository.Add(item);
                _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
		}
	}
}
