using AutoMapper;
using Business_Layer.Extensions;
using Business_Layer.Services.Interfaces;
using Data_Layer.Enums;
using Data_Layer.Models.CategoryModels;
using Data_Layer.PostModels;
using Data_Layer.ResponseObjects;
using Data_Layer.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProgQuizWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllCategories()
        {
            var results = await _service.GetAll();
            if (results is not null && results.Count > 0)
            {
                var mappedResults = _mapper.Map<List<LanguageCategory>, List<CategoryViewModel>>(results);
                return StatusCode(200, mappedResults);
            }
            return StatusCode(204, new ResponseObject(ResponseType.NoResult.GetDisplayNameProperty(), "Не существует ни одной категории!"));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] int id)
        {
            var result = await _service.GetById(id);
            if (result is null)
            {
                return StatusCode(404, new ResponseObject(ResponseType.NoResult.GetDisplayNameProperty(), "Категория не найдена."));
            }
            var mappedResult = _mapper.Map<CategoryViewModel>(result);
            return StatusCode(200, mappedResult);
        }

        [HttpGet]
        [Route("{id}/subcategories")]
        public async Task<IActionResult> GetSubcategories([FromRoute] int id)
        {
            var results = await _service.GetSubcategories(id);
            if (results is null || results.Count == 0)
            {
                return StatusCode(204, new ResponseObject(ResponseType.NoResult.GetDisplayNameProperty(), "Подкатегорий не существует!"));
            }
            var mappedResults = _mapper.Map<List<SubcategoryViewModel>>(results);
            return StatusCode(200, mappedResults);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> AddCategory(CategoryPostModel categoryModel)
        {
            var entityModel = _mapper.Map<LanguageCategory>(categoryModel);
            bool isAdded = await _service.AddCategory(entityModel);
            if (isAdded)
            {
                return StatusCode(201, new ResponseObject(ResponseType.Success.GetDisplayNameProperty(), "Категория создана!"));
            }
            return StatusCode(422, new ResponseObject(ResponseType.Failure.GetDisplayNameProperty(), "Не удалось создать категорию."));
        }
    }
}
