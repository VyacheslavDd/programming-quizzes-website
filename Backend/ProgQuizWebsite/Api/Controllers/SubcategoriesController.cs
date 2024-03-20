using AutoMapper;
using Core.Base.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgQuizWebsite.Api.PostModels;
using ProgQuizWebsite.Api.ViewModels;
using ProgQuizWebsite.Domain.CategoryModels;
using ProgQuizWebsite.Infrastracture.Filters;

namespace ProgQuizWebsite.Api.Controllers
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
            return await AddAsync(subcategoryPostModel, _service, _mapper);
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
            return await DeleteAsync(id, _service);
        }
    }
}
