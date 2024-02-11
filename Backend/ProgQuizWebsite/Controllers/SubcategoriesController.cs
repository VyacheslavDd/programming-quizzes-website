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
    public class SubcategoriesController : BaseController
    {
        private readonly IService<QuizSubcategory> _service;
        private readonly IMapper _mapper;

        public SubcategoriesController(IService<QuizSubcategory> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var results = await _service.GetAllAsync();
            return ProcessItems<QuizSubcategory?, SubcategoryViewModel>(results, _mapper, "Подкатегорий не существует");
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var entry = await _service.GetByIdAsync(id);
            return ProcessItem<QuizSubcategory?, SubcategoryViewModel>(entry, _mapper, "Подкатегории не существует");
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Add(SubcategoryPostModel subcategoryPostModel)
        {
            var model = _mapper.Map<QuizSubcategory>(subcategoryPostModel);
            var isAdded = await _service.AddAsync(model);
            return ProcessAdding(isAdded, "Подкатегория создана",
                "Не удалось создать подкатегорию. Убедитесь, что: категория существует; название подкатегории уникально в пределах категории.");
        }

		[HttpPut]
		[Route("{id}/update")]
		public async Task<IActionResult> Update([FromRoute] int id, SubcategoryPostModel subcategoryModel)
		{
			var entity = await _service.GetByIdAsync(id);
			if (entity is not null)
			{
                entity.Name = subcategoryModel.Name;
                entity.LanguageCategoryId = subcategoryModel.LanguageCategoryId;
			}
			bool isUpdated = await _service.UpdateAsync(entity);
			return ProcessUpdating(isUpdated, "Подкатегория обновлена", "Не удалось обновить подкатегорию." +
                "Проверьте существование подкатегории, уникальность названия в пределах категории");
		}

		[HttpDelete]
		[Route("{id}")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			var isDeleted = await _service.DeteteAsync(id);
			return ProcessDeleting(isDeleted, "Подкатегория удалена", "Не удалось удалить данные! Проверьте существование объекта.");
		}
	}
}
