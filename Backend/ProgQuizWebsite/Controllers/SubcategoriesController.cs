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

namespace ProgQuizWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubcategoriesController : ControllerBase
    {
        private readonly ISubcategoryService _service;
        private readonly IMapper _mapper;

        public SubcategoriesController(ISubcategoryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var results = await _service.GetAll();
            if (results is null || results.Count == 0)
                return StatusCode(404, new ResponseObject(ResponseType.NoResult.GetDisplayNameProperty(), "Подкатегорий не существует!"));
            var viewResults = _mapper.Map<List<SubcategoryViewModel>>(results);
            return StatusCode(200, viewResults);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var entry = await _service.GetSubcategoryById(id);
            if (entry is null)
                return StatusCode(404, new ResponseObject(ResponseType.NoResult.GetDisplayNameProperty(), "Подкатегории не существует!"));
            var mappedEntry = _mapper.Map<SubcategoryViewModel>(entry);
            return StatusCode(200, mappedEntry);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> AddSubcategory(SubcategoryPostModel subcategoryPostModel)
        {
            var model = _mapper.Map<QuizSubcategory>(subcategoryPostModel);
            var isAdded = await _service.AddSubcategory(model);
            if (isAdded)
                return StatusCode(201, new ResponseObject(ResponseType.Success.GetDisplayNameProperty(), "Подкатегория создана!"));
            return StatusCode(422, new ResponseObject(ResponseType.Failure.GetDisplayNameProperty(),
                "Не удалось создать подкатегорию. Убедитесь, что: категория существует; название подкатегории уникально в пределах категории."));
        }
    }
}
