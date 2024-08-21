
using Core.Base.Service.Interfaces;
using Core.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minio;
using ProgQuizWebsite.Api.Quizzes.PostModels;
using ProgQuizWebsite.Infrastracture.Quizzes.Filters;

namespace ProgQuizWebsite.Api.Quizzes.Controllers
{
    /// <summary>
    /// Контроллер для работы с изображениями
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    //[ServiceFilter(typeof(QuizElementsExceptionFilter))]
    public class ImagesController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly IMinioClientFactory _minioClientFactory;

        public ImagesController(IImageService imageService, IMinioClientFactory minioClientFactory)
        {
            _imageService = imageService;
            _minioClientFactory = minioClientFactory;
        }

        /// <summary>
        /// Метод для получения изображения викторины по имени файла
        /// </summary>
        /// <param name="url">Название файла</param>
        /// <returns>Возвращает изображение как массив байтов (base64)</returns>
        [HttpGet]
        [Route("quizzes/{url}")]
        public async Task<IActionResult> GetQuizImageAsByteArray([FromRoute] string url)
        {
            var minioClient = _minioClientFactory.CreateClient();
            return StatusCode(200, await _imageService.GetFileAsByteArrayAsync(minioClient, SpecialConstants.QuizImagesBucketName, url));
        }

        /// <summary>
        /// Метод для получения изображения вопроса по имени файла
        /// </summary>
        /// <param name="url">Название файла полностью</param>
        /// <returns>Возвращает изображение как массив байтов (base64)</returns>
        [HttpGet]
        [Route("questions/{url}")]
        public async Task<IActionResult> GetQuestionImageAsByteArray([FromRoute] string url)
        {
			var minioClient = _minioClientFactory.CreateClient();
			return StatusCode(200, await _imageService.GetFileAsByteArrayAsync(minioClient, SpecialConstants.QuestionImagesBucketName, url));
        }

		/// <summary>
		/// Метод для получения аватарки пользователя по имени файла
		/// </summary>
		/// <param name="url">Название файла полностью</param>
		/// <returns>Возвращает изображение как массив байтов (base64)</returns>
		[HttpGet]
		[Route("users/{url}")]
		public async Task<IActionResult> GetUserImageAsByteArray([FromRoute] string url)
		{
			var minioClient = _minioClientFactory.CreateClient();
			return StatusCode(200, await _imageService.GetFileAsByteArrayAsync(minioClient, SpecialConstants.UserImagesBucketName, url));
		}
	}
}
