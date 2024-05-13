
using Core.Base.Repository;
using Core.Base.Service.Interfaces;
using Core.Redis.Interfaces;
using Microsoft.EntityFrameworkCore;
using ProgQuizWebsite.Infrastracture.Contexts;
using ProgQuizWebsite.Infrastracture.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;
using ProgQuizWebsite.Services.Interfaces;

namespace ProgQuizWebsite.Services.Implementations
{
    public abstract class BaseService<T> : IService<T>, IBaseService<T> where T : class
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IRepository<T> _repository;
        protected readonly IRedisService _redisService;
        protected readonly QuizAppContext _quizAppContext;

        public BaseService(IRepository<T> repository, IUnitOfWork unitOfWork, IRedisService redisService, QuizAppContext quizAppContext)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _redisService = redisService;
            _quizAppContext = quizAppContext;
        }

        public abstract Task ValidateItemDataAsync(T? item);

        public virtual async Task<Guid> AddAsync(T? item)
        {
            await ValidateItemDataAsync(item);
            var guid = await _repository.AddAsync(item);
            await _unitOfWork.SaveAsync();
            await UpdateCache();
            return guid;
        }

        public virtual async Task DeteteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
            await UpdateCache();
        }

        public virtual async Task<List<T?>> GetAllAsync()
        {
            var name = typeof(T).Name;
            var (results, response) = await _redisService.Get<List<T>>(name);
            if (results == null)
            {
                var entries = await _repository.GetAllAsync();
                if (response == RedisServiceResponse.Success)
                    await _redisService.Set(name, entries);
                return entries;
            }
            foreach (var result in results)
                _quizAppContext.Set<T>().Attach(result);
            return results;
        }

        public virtual async Task<T?> GetByGuidAsync(Guid id)
        {
            return await _repository.GetByGuidAsync(id);
        }

        public virtual async Task UpdateAsync(T? item)
        {
            await ValidateItemDataAsync(item);
            await _unitOfWork.SaveAsync();
            await UpdateCache();
        }

		public async Task UpdateCache()
		{
            var name = typeof(T).Name;
            var entries = await _repository.GetAllAsync();
            await _redisService.Set(name, entries);
		}
	}
}
