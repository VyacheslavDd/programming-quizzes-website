using AutoMapper;
using Business_Layer.Services.Interfaces;
using Data_Layer.Models.QuizContentModels;
using Data_Layer.PostModels;
using Data_Layer.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ProgQuizWebsite.Controllers
{
	[ApiController]
	[Route("/api/[controller]")]
	public class QuestionsController : BaseController
	{
		private readonly IService<Question> _service;
		private readonly IMapper _mapper;

		public QuestionsController(IService<Question> service, IMapper mapper)
		{
			_service = service;
			_mapper = mapper;
		}

		[HttpGet]
		[Route("all")]
		public async Task<IActionResult> GetAll()
		{
			var questions = await _service.GetAllAsync();
			return ProcessItems<Question, QuestionViewModel>(questions, _mapper, "База вопросов пуста.");
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> GetById([FromRoute] int id)
		{
			var question = await _service.GetByIdAsync(id);
			return ProcessItem<Question, QuestionViewModel>(question, _mapper, "Вопрос не существует");
		}

		[HttpPost]
		[Route("create")]
		public async Task<IActionResult> Add(QuestionPostModel postModel)
		{
			var mappedQuestion = _mapper.Map<Question>(postModel);
			var isAdded = await _service.AddAsync(mappedQuestion);
			return ProcessAdding(isAdded, "Вопрос добавлен", "Не удалось добавить вопрос. Проверьте уникальность вопроса и существование викторины");
		}

		[HttpPut]
		[Route("{id}/update")]
		public async Task<IActionResult> Update([FromRoute] int id, QuestionPostModel questionModel)
		{
			var entity = await _service.GetByIdAsync(id);
			if (entity is not null)
			{
				entity.Title = questionModel.Title;
				entity.Description = questionModel.Description;
				entity.SuccessInfo = questionModel.SuccessInfo;
				entity.FailureInfo = questionModel.FailureInfo;
				entity.QuizId = questionModel.QuizId;
				entity.Type = questionModel.Type;
			}
			bool isUpdated = await _service.UpdateAsync(entity);
			return ProcessUpdating(isUpdated, "Вопрос обновлен", "Не удалось обновить вопрос. Проверьте уникальность вопроса и существование викторины");
		}

		[HttpDelete]
		[Route("{id}")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			var isDeleted = await _service.DeteteAsync(id);
			return ProcessDeleting(isDeleted, "Вопрос удалён", "Не удалось удалить данные! Проверьте существование объекта.");
		}
	}
}
