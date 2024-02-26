﻿using Business_Layer.Services.Interfaces;
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

        public abstract Task ValidateItemData(T? item);

        public async virtual Task<Guid> AddAsync(T? item)
        {
            await ValidateItemData(item);
            var guid = await _repository.AddAsync(item);
            await _unitOfWork.SaveAsync();
            return guid;
        }

        public virtual async Task DeteteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public virtual async Task<List<T?>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public virtual async Task<T?> GetByGuidAsync(Guid id)
        {
            return await _repository.GetByGuidAsync(id);
        }

        public virtual async Task UpdateAsync(T? item)
        {
            await ValidateItemData(item);
            await _unitOfWork.SaveAsync();
        }
	}
}
