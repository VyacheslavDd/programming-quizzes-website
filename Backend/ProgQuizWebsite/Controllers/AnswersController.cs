using AutoMapper;
using Business_Layer.Services.Interfaces;
using Data_Layer.Models.QuizContentModels;
using Data_Layer.PostModels;
using Data_Layer.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProgQuizWebsite.Controllers
{
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

		[HttpGet]
		[Route("all")]
		public async Task<IActionResult> GetAll()
		{
			var answers = await _service.GetAllAsync();
			return ProcessItems<Answer, AnswerViewModel>(answers, _mapper, "База ответов пуста");
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> GetById([FromRoute] int id)
		{
			var answer = await _service.GetByIdAsync(id);
			return ProcessItem<Answer, AnswerViewModel>(answer, _mapper, "Ответ не существует");
		}

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

		[HttpDelete]
		[Route("{id}")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			var isDeleted = await _service.DeteteAsync(id);
			return ProcessDeleting(isDeleted, "Ответ удалён", "Не удалось удалить данные! Проверьте существование объекта.");
		}
	}
}
