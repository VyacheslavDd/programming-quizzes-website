﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgQuizWebsite.Api.Quizzes.PostModels;
using ProgQuizWebsite.Api.Quizzes.ViewModels;
using ProgQuizWebsite.Domain.Quizzes.Models.CategoryModels;
using ProgQuizWebsite.Infrastracture.Quizzes.Filters;
using ProgQuizWebsite.Services.Quizzes.Interfaces;

namespace ProgQuizWebsite.Api.Quizzes.Controllers
{
    /// <summary>
    /// Контроллер для работы с категориями викторин (Python, C#...)
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(QuizElementsExceptionFilter))]
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService _service;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Метод для получения всех категорий. Включает в себя существующие подкатегории, не включает викторины, соответствующие категории
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return await GetAllAsync<LanguageCategory, CategoryViewModel>(_service, _mapper, cancellationToken);
        }
        /// <summary>
        /// Метод для получения категории. Включает в себя существующие подкатегории, и викторины, соответствующие категории
        /// </summary>
        /// <param name="id">Guid категории</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _service.GetByGuidAsync(id);
            var mappedResult = _mapper.Map<CategoryViewModel>(result);
            var quizViewModels = _mapper.Map<List<QuizViewModel>>(result.Quizzes);
            mappedResult.Quizzes = quizViewModels;
            return StatusCode(200, mappedResult);
        }

        /// <summary>
        /// Метод для получения всех подкатегорий указанной категории
        /// </summary>
        /// <param name="id">Guid категории</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}/subcategories")]
        public async Task<IActionResult> GetSubcategoriesAsync([FromRoute] Guid id)
        {
            var subcategories = await _service.GetConnectedSubcategoriesAsync(id);
            var mappedSubcategories = _mapper.Map<List<SubcategoryViewModel>>(subcategories);
            return Ok(mappedSubcategories);
        }

        /// <summary>
        /// Метод для добавления новой категории
        /// </summary>
        /// <param name="categoryModel">Модель категории. Нужно указать название категории</param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
		[Authorize(Roles = "Admin,Redactor")]
		public async Task<IActionResult> Add(CategoryPostModel categoryModel)
        {
            return await AddAsync(categoryModel, _service, _mapper);
        }
        /// <summary>
        /// Метод для обновления категории
        /// </summary>
        /// <param name="id">Guid категории</param>
        /// <param name="categoryModel">Модель категории. Нужно указать название категории</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/update")]
		[Authorize(Roles = "Admin,Redactor")]
		public async Task<IActionResult> Update([FromRoute] Guid id, CategoryPostModel categoryModel)
        {
            var entity = await _service.GetByGuidAsync(id);
            entity.Name = categoryModel.Name;
            await _service.UpdateAsync(entity);
            return StatusCode(200);
        }
        /// <summary>
        /// Метод для удаления категории
        /// </summary>
        /// <param name="id">Guid категории</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
		[Authorize(Roles = "Admin,Redactor")]
		public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            return await DeleteAsync(id, _service);
        }
    }
}
