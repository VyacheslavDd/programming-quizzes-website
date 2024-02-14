using Business_Layer.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Data_Layer.Constants;
using Microsoft.AspNetCore.Mvc;

namespace ProgQuizWebsite.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ImagesController : ControllerBase
	{
		private readonly IImageService _imageService;
		private readonly IWebHostEnvironment _environment;

		public ImagesController(IImageService imageService, IWebHostEnvironment environment)
		{
			_imageService = imageService;
			_environment = environment;
		}


		[HttpGet]
		[Route("{url}")]
		public async Task<IActionResult> GetImageAsByteArray([FromRoute] string url)
		{
			return StatusCode(200, await _imageService.GetFileAsByteArrayAsync(_environment.ContentRootPath, SpecialConstants.QuizImagesDirectoryName, url));
		}
	}
}
