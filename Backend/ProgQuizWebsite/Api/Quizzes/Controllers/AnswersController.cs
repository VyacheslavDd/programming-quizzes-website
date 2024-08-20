using AutoMapper;
using ProgQuizWebsite.Services.Quizzes.Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Base.Service.Interfaces;
using MassTransit;
using ProgQuizWebsite.Api.Quizzes.PostModels;
using ProgQuizWebsite.Api.Quizzes.ViewModels;
using ProgQuizWebsite.Domain.Quizzes.Models.QuizContentModels;
using ProgQuizWebsite.Infrastracture.Quizzes.Filters;
using Minio;

namespace ProgQuizWebsite.Api.Quizzes.Controllers
{
    /// <summary>
    /// Контроллер для работы с базой ответов
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(QuizElementsExceptionFilter))]
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
            return await GetAllAsync<Answer, AnswerViewModel>(_service, _mapper);
        }
        /// <summary>
        /// Метод для получения ответа
        /// </summary>
        /// <param name="id">Guid ответа</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            return await GetByIdAsync<Answer, AnswerViewModel>(id, _service, _mapper);
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
            return await AddAsync(postModel, _service, _mapper);
        }
        /// <summary>
        /// Метод для обновления информации ответа
        /// </summary>
        /// <param name="id">Guid ответа</param>
        /// <param name="answerModel">Модель ответа. Нужно указать: название, правильный ли ответ, Id существующего вопроса</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/update")]
        public async Task<IActionResult> Update([FromRoute] Guid id, AnswerPostModel answerModel)
        {
            var entity = await _service.GetByGuidAsync(id);
            entity.Name = answerModel.Name;
            entity.IsCorrect = answerModel.IsCorrect;
            entity.QuestionId = answerModel.QuestionId;
            await _service.UpdateAsync(entity);
            return StatusCode(200);
        }
        /// <summary>
        /// Метод для удаления ответа
        /// </summary>
        /// <param name="id">Guid ответа, который нужно удалить</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            return await DeleteAsync(id, _service);
        }
    }
}
