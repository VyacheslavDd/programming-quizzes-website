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
    public class CategoriesController : BaseController
    {
        private readonly IService<LanguageCategory> _service;
        private readonly IMapper _mapper;

        public CategoriesController(IService<LanguageCategory> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var results = await _service.GetAllAsync();
            return ProcessItems<LanguageCategory?, CategoryViewModel>(results, _mapper, "Не существует ни одной категории");
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result is null)
            {
                return StatusCode(404, new ResponseObject(ResponseType.NoResult.GetDisplayNameProperty(), "Категория не найдена."));
            }
            var mappedResult = _mapper.Map<CategoryViewModel>(result);
            var quizViewModels = _mapper.Map<List<QuizViewModel>>(result.Quizzes);
            mappedResult.Quizzes = quizViewModels;
            return StatusCode(200, mappedResult);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Add(CategoryPostModel categoryModel)
        {
            var entityModel = _mapper.Map<LanguageCategory>(categoryModel);
            bool isAdded = await _service.AddAsync(entityModel);
            return ProcessAdding(isAdded, "Категория создана", "Не удалось создать категорию.");
        }

        [HttpPut]
        [Route("{id}/update")]
        public async Task<IActionResult> Update([FromRoute] int id, CategoryPostModel categoryModel)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity is not null)
            {
                entity.Name = categoryModel.Name;
            }
            bool isUpdated = await _service.UpdateAsync(entity);
			return ProcessUpdating(isUpdated, "Категория обновлена", "Не удалось обновить категорию. Проверьте существование категории");
		}

		[HttpDelete]
		[Route("{id}")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			var isDeleted = await _service.DeteteAsync(id);
			return ProcessDeleting(isDeleted, "Категория удалена", "Не удалось удалить данные! Проверьте существование объекта.");
		}
	}
}
