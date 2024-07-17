
using Core.Base.Service.Interfaces;
using Core.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgQuizWebsite.Infrastracture.Quizzes.Filters;

namespace ProgQuizWebsite.Api.Quizzes.Controllers
{
    /// <summary>
    /// Контроллер для работы с изображениями
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(QuizElementsExceptionFilter))]
    public class ImagesController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly IWebHostEnvironment _environment;

        public ImagesController(IImageService imageService, IWebHostEnvironment environment)
        {
            _imageService = imageService;
            _environment = environment;
        }

        /// <summary>
        /// Метод для получения изображения викторины по имени файла
        /// </summary>
        /// <param name="url">Название файла полностью, с расширением</param>
        /// <returns>Возвращает изображение как массив байтов (base64)</returns>
        [HttpGet]
        [Route("quiz/{url}")]
        public async Task<IActionResult> GetQuizImageAsByteArray([FromRoute] string url)
        {
            return StatusCode(200, await _imageService.GetFileAsByteArrayAsync(_environment.ContentRootPath, SpecialConstants.QuizImagesDirectoryName, url));
        }

        /// <summary>
        /// Метод для получения изображения вопроса по имени файла
        /// </summary>
        /// <param name="url">Название файла полностью, с расширением</param>
        /// <returns>Возвращает изображение как массив байтов (base64)</returns>
        [HttpGet]
        [Route("question/{url}")]
        public async Task<IActionResult> GetQuestionImageAsByteArray([FromRoute] string url)
        {
            return StatusCode(200, await _imageService.GetFileAsByteArrayAsync(_environment.ContentRootPath, SpecialConstants.QuestionImagesDirectoryName, url));
        }
    }
}
