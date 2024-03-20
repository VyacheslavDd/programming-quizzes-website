using AutoMapper;
using Core.Base.Service.Interfaces;
using Core.Constants;
using Microsoft.AspNetCore.Mvc;
using ProgQuizWebsite.Api.PostModels;
using ProgQuizWebsite.Api.ViewModels;
using ProgQuizWebsite.Domain.QuizContentModels;
using ProgQuizWebsite.Infrastracture.Filters;

namespace ProgQuizWebsite.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с вопросами для викторин
    /// </summary>
    [ApiController]
    [Route("/api/[controller]")]
    [ServiceFilter(typeof(QuizElementsExceptionFilter))]
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
            return await GetAllAsync<Question, QuestionViewModel>(_service, _mapper);
        }
        /// <summary>
        /// Метод для получения вопроса. Включает ответы на вопрос
        /// </summary>
        /// <param name="id">Guid вопроса</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            return await GetByIdAsync<Question, QuestionViewModel>(id, _service, _mapper);
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
            var entityGuid = await _service.AddAsync(mappedQuestion);
            if (entityGuid != Guid.Empty) await _imageService.SaveFileAsync(postModel.Image, _environment.ContentRootPath, SpecialConstants.QuestionImagesDirectoryName, path);
            return StatusCode(201, entityGuid);
        }
        /// <summary>
        /// Метод для обновления вопроса
        /// </summary>
        /// <param name="id">Guid вопроса</param>
        /// <param name="questionModel">Модель вопроса. Нужно указать: тип, название, описание к заданию, текст при правильном и неправильном ответе, Id существующей викторины, картинку</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/update")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromForm] QuestionPostModel questionModel)
        {
            var path = _imageService.CreateName(questionModel.Image.FileName);
            var entity = await _service.GetByGuidAsync(id);
            _imageService.DeleteFile(_environment.ContentRootPath, SpecialConstants.QuestionImagesDirectoryName, entity.ImageUrl);
            entity.Title = questionModel.Title;
            entity.Description = questionModel.Description;
            entity.SuccessInfo = questionModel.SuccessInfo;
            entity.FailureInfo = questionModel.FailureInfo;
            entity.QuizId = questionModel.QuizId;
            entity.Type = questionModel.Type;
            entity.ImageUrl = path;
            await _service.UpdateAsync(entity);
            await _imageService.SaveFileAsync(questionModel.Image, _environment.ContentRootPath, SpecialConstants.QuestionImagesDirectoryName, path);
            return StatusCode(200);
        }
        /// <summary>
        /// Метод для удаления вопроса
        /// </summary>
        /// <param name="id">Guid вопроса</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var question = await _service.GetByGuidAsync(id);
            await _service.DeteteAsync(id);
            _imageService.DeleteFile(_environment.ContentRootPath, SpecialConstants.QuestionImagesDirectoryName, question.ImageUrl);
            return StatusCode(200);
        }
    }
}
