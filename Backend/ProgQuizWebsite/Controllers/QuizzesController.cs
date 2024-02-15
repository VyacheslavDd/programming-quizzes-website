using AutoMapper;
using Business_Layer.Services.Interfaces;
using Data_Layer.Models.QuizModels;
using Data_Layer.PostModels;
using Data_Layer.ResponseObjects;
using Data_Layer.Enums;
using Data_Layer.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business_Layer.Extensions;
using Data_Layer.ViewModels;
using Data_Layer.FilterModels.QuizFilters;

namespace ProgQuizWebsite.Controllers
{
    /// <summary>
    /// Контроллер для работы с викторинами
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class QuizzesController : BaseController
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IImageService _imageService;
        private readonly IQuizService _service;
        private readonly IMapper _mapper;

        public QuizzesController(IQuizService service, IMapper mapper, IWebHostEnvironment environment, IImageService imageService)
        {
            _service = service;
            _mapper = mapper;
            _environment = environment;
            _imageService = imageService;
        }
        /// <summary>
        /// Метод для добавления викторины
        /// </summary>
        /// <param name="model">Модель викторины. Нужно указать: название, описание, Id категории, сложность, Id подкатегорий, обложку викторины</param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
		[Consumes("multipart/form-data")]
		public async Task<IActionResult> Add([FromForm] QuizPostModel model)
        {
            var path = _imageService.CreateName(model.QuizImage.FileName);
            var mappedModel = _mapper.Map<Quiz>(model);
            mappedModel.ImageUrl = path;
            var isAdded = await _service.AddAsync(mappedModel, model.SubcategoriesId);
            if (isAdded) await _imageService.SaveFileAsync(model.QuizImage, _environment.ContentRootPath, SpecialConstants.QuizImagesDirectoryName, path);
			return ProcessAdding(isAdded, "Объект викторины создан!",
                "Не удалось создать викторину. Проверьте идентификаторы категории и подкатегорий, уникальность викторины.");
        }
        /// <summary>
        /// Метод для получения всех викторин. Не включает вопросы с ответами
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var results = await _service.GetAllAsync();
            return ProcessItems<Quiz, QuizViewModel>(results, _mapper, "Викторины отсутствуют");
		}
        /// <summary>
        /// Метод для получения всех викторин в соответствии с заданным фильтром
        /// </summary>
        /// <param name="filter">Модель фильтра. Нужно указать: кол-во викторин на страницу, желаемую страницу</param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetPage([FromQuery] GetQuizzesFilter filter)
        {
            var results = await _service.GetByPageFilter(filter);
			return ProcessItems<Quiz?, QuizViewModel>(results, _mapper, "Empty");
		}
        /// <summary>
        /// Метод для получения викторины. Включает вопросы с ответами
        /// </summary>
        /// <param name="id">Id викторины</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result is null)
                return StatusCode(404, new ResponseObject(ResponseType.NoResult.GetDisplayNameProperty(), "Викторины не существует"));
            var mappedResult = _mapper.Map<QuizViewModel>(result);
            mappedResult.Subcategories = _mapper.Map<List<SubcategoryViewModel>>(result.Subcategories);
            mappedResult.Questions = _mapper.Map<List<QuestionViewModel>>(result.Questions);
            return StatusCode(200, mappedResult);
        }
		/// <summary>
		/// Метод для обновления викторины
		/// </summary>
		/// <param name="id">Id викторины</param>
		/// <param name="quizModel">Модель викторины. Нужно указать: название, описание, Id категории, сложность, Id подкатегорий, обложку викторины</param>
		/// <returns></returns>
		[HttpPut]
		[Route("{id}/update")]
        [Consumes("multipart/form-data")]
		public async Task<IActionResult> Update([FromRoute] int id, [FromForm] QuizPostModel quizModel)
		{
			var entity = await _service.GetByIdAsync(id);
            var path = _imageService.CreateName(quizModel.QuizImage.FileName);
			if (entity is not null)
			{
				entity.Title = quizModel.Title;
                entity.Description = quizModel.Description;
                entity.LanguageCategoryId = quizModel.LanguageCategoryId;
                entity.Difficulty = quizModel.Difficulty;
                entity.ImageUrl = path;
			}
			bool isUpdated = await _service.MatchSubcategories(entity, quizModel.SubcategoriesId) && await _service.UpdateAsync(entity);
            if (isUpdated) await _imageService.SaveFileAsync(quizModel.QuizImage, _environment.ContentRootPath, SpecialConstants.QuizImagesDirectoryName, path);
			return ProcessUpdating(isUpdated, "Викторина обновлена", "Не удалось обновить викторину." +
                "Проверьте уникальность, идентификаторы категории и подкатегорий");
		}
        /// <summary>
        /// Метод для удаления викторины
        /// </summary>
        /// <param name="id">Id викторины</param>
        /// <returns></returns>
		[HttpDelete]
		[Route("{id}")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
            var quiz = await _service.GetByIdAsync(id);
			var isDeleted = await _service.DeteteAsync(id);
            if (isDeleted) _imageService.DeleteFile(_environment.ContentRootPath, SpecialConstants.QuizImagesDirectoryName, quiz.ImageUrl);
			return ProcessDeleting(isDeleted, "Викторина удалена", "Не удалось удалить данные! Проверьте существование объекта.");
		}
	}
}
