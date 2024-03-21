using AutoMapper;
using Core.Base.Service.Interfaces;
using Core.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgQuizWebsite.Api.PostModels;
using ProgQuizWebsite.Api.ViewModels;
using ProgQuizWebsite.Domain.FilterModels;
using ProgQuizWebsite.Domain.QuizModels;
using ProgQuizWebsite.Infrastracture.Filters;
using ProgQuizWebsite.Services.Interfaces;

namespace ProgQuizWebsite.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с викторинами
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(QuizElementsExceptionFilter))]
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
            var entityGuid = await _service.AddAsync(mappedModel, model.SubcategoriesId);
            if (entityGuid != Guid.Empty) await _imageService.SaveFileAsync(model.QuizImage, _environment.ContentRootPath, SpecialConstants.QuizImagesDirectoryName, path);
            return StatusCode(201, entityGuid);
        }
        /// <summary>
        /// Метод для получения всех викторин. Не включает вопросы с ответами
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            return await GetAllAsync<Quiz, QuizViewModel>(_service, _mapper);
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
            var results = await _service.GetByFilterAsync(filter, Response);
            var models = _mapper.Map<List<QuizViewModel>>(results);
            return StatusCode(200, models);
        }
        /// <summary>
        /// Метод для получения викторины. Включает вопросы с ответами
        /// </summary>
        /// <param name="id">Guid викторины</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _service.GetByGuidAsync(id);
            var mappedResult = _mapper.Map<QuizViewModel>(result);
            mappedResult.Subcategories = _mapper.Map<List<SubcategoryViewModel>>(result.Subcategories);
            mappedResult.Questions = _mapper.Map<List<QuestionViewModel>>(result.Questions);
            return StatusCode(200, mappedResult);
        }
        /// <summary>
        /// Метод для обновления викторины
        /// </summary>
        /// <param name="id">Guid викторины</param>
        /// <param name="quizModel">Модель викторины. Нужно указать: название, описание, Id категории, сложность, Id подкатегорий, обложку викторины</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/update")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromForm] QuizPostModel quizModel)
        {
            var entity = await _service.GetByGuidAsync(id);
            var path = _imageService.CreateName(quizModel.QuizImage.FileName);
            _imageService.DeleteFile(_environment.ContentRootPath, SpecialConstants.QuestionImagesDirectoryName, entity.ImageUrl);
            entity.Title = quizModel.Title;
            entity.Description = quizModel.Description;
            entity.LanguageCategoryId = quizModel.LanguageCategoryId;
            entity.Difficulty = quizModel.Difficulty;
            entity.ImageUrl = path;
            await _service.MatchSubcategoriesAsync(entity, quizModel.SubcategoriesId);
            await _service.UpdateAsync(entity);
            await _imageService.SaveFileAsync(quizModel.QuizImage, _environment.ContentRootPath, SpecialConstants.QuizImagesDirectoryName, path);
            return StatusCode(200);
        }
        /// <summary>
        /// Метод для удаления викторины
        /// </summary>
        /// <param name="id">Guid викторины</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var quiz = await _service.GetByGuidAsync(id);
            await _service.DeteteAsync(id);
            _imageService.DeleteFile(_environment.ContentRootPath, SpecialConstants.QuizImagesDirectoryName, quiz.ImageUrl);
            return StatusCode(200);
        }
    }
}
