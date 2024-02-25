﻿using AutoMapper;
using Business_Layer.Extensions;
using Business_Layer.Services.Interfaces;
using Data_Layer.Enums;
using Data_Layer.Models.QuizContentModels;
using Data_Layer.ResponseObjects;
using Data_Layer.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ProgQuizWebsite.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        public async Task<IActionResult> GetAllAsync<T, V>(IService<T> service, IMapper mapper) where T: class
        {
            var entities = await service.GetAllAsync();
            var models = mapper.Map<List<V>>(entities);
            return StatusCode(200, models);
        }

        public async Task<IActionResult> GetByIdAsync<T, V>(Guid guid, IService<T> service, IMapper mapper) where T : class
        {
            var entity = await service.GetByGuidAsync(guid);
            var model = mapper.Map<V>(entity);
            return StatusCode(200, model);
        }

        public async Task<IActionResult> AddAsync<T, V>(T model, IService<V> service, IMapper mapper) where V: class
        {
			var entity = mapper.Map<V>(model);
			var entityGuid = await service.AddAsync(entity);
			return StatusCode(201, entityGuid);
		}

        public async Task<IActionResult> DeleteAsync<T>(Guid id, IService<T> service) where T: class
        {
            await service.DeteteAsync(id);
            return StatusCode(200);
        }
	}
}
