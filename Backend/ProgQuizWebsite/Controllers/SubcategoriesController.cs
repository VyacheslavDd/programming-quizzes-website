using AutoMapper;
using Business_Layer.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Data_Layer.Enums;
using Data_Layer.PostModels;
using Data_Layer.ResponseObjects;
using Business_Layer.Extensions;
using Data_Layer.ViewModels;
using Data_Layer.Models.CategoryModels;
using Data_Layer.Models.QuizContentModels;
using ProgQuizWebsite.ActionFilters;

namespace ProgQuizWebsite.Controllers
{
    /// <summary>
    /// Контроллер для работы с подкатегориями (ASP.Net Core...)
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
	[ServiceFilter(typeof(QuizElementsExceptionFilter))]
	public class SubcategoriesController : BaseController
    {
        private readonly IService<QuizSubcategory> _service;
        private readonly IMapper _mapper;

        public SubcategoriesController(IService<QuizSubcategory> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        /// <summary>
        /// Метод для получения всех подкатегорий
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            return await GetAllAsync<QuizSubcategory, SubcategoryViewModel>(_service, _mapper);
		}
		/// <summary>
		/// Метод для получения подкатегории
		/// </summary>
		/// <param name="id">Guid подкатегории</param>
		/// <returns></returns>
		[HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            return await GetByIdAsync<QuizSubcategory, SubcategoryViewModel>(id, _service, _mapper);
        }
        /// <summary>
        /// Метод для добавления подкатегории
        /// </summary>
        /// <param name="subcategoryPostModel">Модель подкатегории. Нужно указать: название, Id категории</param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Add(SubcategoryPostModel subcategoryPostModel)
        {
            return await AddAsync<SubcategoryPostModel, QuizSubcategory>(subcategoryPostModel, _service, _mapper);
        }
		/// <summary>
		/// Метод для обновления подкатегории
		/// </summary>
		/// <param name="id">Guid подкатегории</param>
		/// <param name="subcategoryModel">Модель подкатегории. Нужно указать: название, Id категории</param>
		/// <returns></returns>
		[HttpPut]
		[Route("{id}/update")]
		public async Task<IActionResult> Update([FromRoute] Guid id, SubcategoryPostModel subcategoryModel)
		{
			var entity = await _service.GetByGuidAsync(id);
            entity.Name = subcategoryModel.Name;
            entity.LanguageCategoryId = subcategoryModel.LanguageCategoryId;
			await _service.UpdateAsync(entity);
            return StatusCode(200);
		}
		/// <summary>
		/// Удаление подкатегории
		/// </summary>
		/// <param name="id">Guid подкатегории</param>
		/// <returns></returns>
		[HttpDelete]
		[Route("{id}")]
		public async Task<IActionResult> Delete([FromRoute] Guid id)
		{
            return await DeleteAsync<QuizSubcategory>(id, _service);
		}
	}
}
