using AutoMapper;
using Business_Layer.Services.Interfaces;
using Data_Layer.Models.QuizContentModels;
using Data_Layer.PostModels;
using Data_Layer.ViewModels;
using Data_Layer.Constants;
using Microsoft.AspNetCore.Mvc;

namespace ProgQuizWebsite.Controllers
{
	/// <summary>
	/// Контроллер для работы с вопросами для викторин
	/// </summary>
	[ApiController]
	[Route("/api/[controller]")]
	public class QuestionsController : BaseController
	{
		private readonly IService<Question> _service;
		private readonly IWebHostEnvironment _environment;
		private readonly IImageService _imageService;
		private readonly IMapper _mapper;

		public QuestionsController(IService<Question> service, IMapper mapper, IWebHostEnvironment environment, IImageService imageService)
		{
			_service = service;
			_mapper = mapper;
			_environment = environment;
			_imageService = imageService;
		}
		/// <summary>
		/// Метод для получения всех вопросов для всех викторин. Не включает ответы на вопрос
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("all")]
		public async Task<IActionResult> GetAll()
		{
			var questions = await _service.GetAllAsync();
			return ProcessItems<Question, QuestionViewModel>(questions, _mapper, "База вопросов пуста.");
		}
		/// <summary>
		/// Метод для получения вопроса. Включает ответы на вопрос
		/// </summary>
		/// <param name="id">Id вопроса</param>
		/// <returns></returns>
		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> GetById([FromRoute] int id)
		{
			var question = await _service.GetByIdAsync(id);
			return ProcessItem<Question, QuestionViewModel>(question, _mapper, "Вопрос не существует");
		}
		/// <summary>
		/// Метод для добавления нового вопроса
		/// </summary>
		/// <param name="postModel">Модель вопроса. Нужно указать: тип, название, описание к заданию, текст при правильном и неправильном ответе, Id существующей викторины, картинку</param>
		/// <returns></returns>
		[HttpPost]
		[Route("create")]
		[Consumes("multipart/form-data")]
		public async Task<IActionResult> Add([FromForm] QuestionPostModel postModel)
		{
			var path = _imageService.CreateName(postModel.Image.FileName);
			var mappedQuestion = _mapper.Map<Question>(postModel);
			mappedQuestion.ImageUrl = path;
			var isAdded = await _service.AddAsync(mappedQuestion);
			if (isAdded) await _imageService.SaveFileAsync(postModel.Image, _environment.ContentRootPath, SpecialConstants.QuestionImagesDirectoryName, path);
			return ProcessAdding(isAdded, "Вопрос добавлен", "Не удалось добавить вопрос. Проверьте уникальность вопроса и существование викторины");
		}
		/// <summary>
		/// Метод для обновления вопроса
		/// </summary>
		/// <param name="id">Id вопроса</param>
		/// <param name="questionModel">Модель вопроса. Нужно указать: тип, название, описание к заданию, текст при правильном и неправильном ответе, Id существующей викторины, картинку</param>
		/// <returns></returns>
		[HttpPut]
		[Route("{id}/update")]
		[Consumes("multipart/form-data")]
		public async Task<IActionResult> Update([FromRoute] int id, [FromForm] QuestionPostModel questionModel)
		{
			var path = _imageService.CreateName(questionModel.Image.FileName);
			var entity = await _service.GetByIdAsync(id);
			if (entity is not null)
			{
				entity.Title = questionModel.Title;
				entity.Description = questionModel.Description;
				entity.SuccessInfo = questionModel.SuccessInfo;
				entity.FailureInfo = questionModel.FailureInfo;
				entity.QuizId = questionModel.QuizId;
				entity.Type = questionModel.Type;
				entity.ImageUrl = path;
			}
			bool isUpdated = await _service.UpdateAsync(entity);
			if (isUpdated) await _imageService.SaveFileAsync(questionModel.Image, _environment.ContentRootPath, SpecialConstants.QuestionImagesDirectoryName, path);
			return ProcessUpdating(isUpdated, "Вопрос обновлен", "Не удалось обновить вопрос. Проверьте уникальность вопроса и существование викторины");
		}
		/// <summary>
		/// Метод для удаления вопроса
		/// </summary>
		/// <param name="id">Id вопроса</param>
		/// <returns></returns>
		[HttpDelete]
		[Route("{id}")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			var question = await _service.GetByIdAsync(id);
			var isDeleted = await _service.DeteteAsync(id);
			if (isDeleted) _imageService.DeleteFile(_environment.ContentRootPath, SpecialConstants.QuestionImagesDirectoryName, question.ImageUrl);
			return ProcessDeleting(isDeleted, "Вопрос удалён", "Не удалось удалить данные! Проверьте существование объекта.");
		}
	}
}
