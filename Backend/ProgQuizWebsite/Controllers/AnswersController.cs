using AutoMapper;
using Business_Layer.Services.Interfaces;
using Data_Layer.Models.QuizContentModels;
using Data_Layer.PostModels;
using Data_Layer.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProgQuizWebsite.Controllers
{
	/// <summary>
	/// Контроллер для работы с базой ответов
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class AnswersController : BaseController
	{
		private readonly IService<Answer> _service;
		private readonly IMapper _mapper;

		public AnswersController(IService<Answer> service, IMapper mapper)
		{
			_service = service;
			_mapper = mapper;
		}

		/// <summary>
		/// Метод для получения всех ответов для всех вопросов
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("all")]
		public async Task<IActionResult> GetAll()
		{
			var answers = await _service.GetAllAsync();
			return ProcessItems<Answer, AnswerViewModel>(answers, _mapper, "База ответов пуста");
		}
		/// <summary>
		/// Метод для получения ответа
		/// </summary>
		/// <param name="id">Id ответа</param>
		/// <returns></returns>
		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> GetById([FromRoute] int id)
		{
			var answer = await _service.GetByIdAsync(id);
			return ProcessItem<Answer, AnswerViewModel>(answer, _mapper, "Ответ не существует");
		}
		/// <summary>
		/// Метод для удаления нового ответа
		/// </summary>
		/// <param name="postModel">Модель ответа. Нужно указать: название, правильный ли ответ, Id существующего вопроса</param>
		/// <returns></returns>
		[HttpPost]
		[Route("create")]
		public async Task<IActionResult> Add(AnswerPostModel postModel)
		{
			var mappedQuestion = _mapper.Map<Answer>(postModel);
			var isAdded = await _service.AddAsync(mappedQuestion);
			return ProcessAdding(isAdded, "Ответ добавлен",
				"Не удалось добавить ответ. Проверьте уникальность ответа," +
				" существование вопроса, соответствие кол-ва правильных ответов к типу вопроса, количество ответов");
		}
		/// <summary>
		/// Метод для обновления информации ответа
		/// </summary>
		/// <param name="id">Id ответа</param>
		/// <param name="answerModel">Модель ответа. Нужно указать: название, правильный ли ответ, Id существующего вопроса</param>
		/// <returns></returns>
		[HttpPut]
		[Route("{id}/update")]
		public async Task<IActionResult> Update([FromRoute] int id, AnswerPostModel answerModel)
		{
			var entity = await _service.GetByIdAsync(id);
			if (entity is not null)
			{
				entity.Name = answerModel.Name;
				entity.IsCorrect = answerModel.IsCorrect;
				entity.QuestionId = answerModel.QuestionId;
			}
			bool isUpdated = await _service.UpdateAsync(entity);
			return ProcessUpdating(isUpdated, "Ответ обновлён", "Не удалось обновить ответ. Проверьте уникальность," +
				"существование вопроса, соответствие кол-ва правильных ответов к типу вопроса, количество ответов");
		}
		/// <summary>
		/// Метод для удаления ответа
		/// </summary>
		/// <param name="id">Id ответа, который нужно удалить</param>
		/// <returns></returns>
		[HttpDelete]
		[Route("{id}")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			var isDeleted = await _service.DeteteAsync(id);
			return ProcessDeleting(isDeleted, "Ответ удалён", "Не удалось удалить данные! Проверьте существование объекта.");
		}
	}
}
