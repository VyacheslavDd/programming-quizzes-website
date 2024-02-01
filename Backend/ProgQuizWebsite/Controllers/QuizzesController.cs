using AutoMapper;
using Business_Layer.Services.Interfaces;
using Data_Layer.Models.QuizModels;
using Data_Layer.PostModels;
using Data_Layer.ResponseObjects;
using Data_Layer.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business_Layer.Extensions;
using Data_Layer.ViewModels;

namespace ProgQuizWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizzesController : ControllerBase
    {
        private readonly IQuizService _service;
        private readonly IMapper _mapper;

        public QuizzesController(IQuizService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> AddQuiz(QuizPostModel model)
        {
            var mappedModel = _mapper.Map<Quiz>(model);
            var isAdded = await _service.AddQuiz(mappedModel, model.SubcategoriesId);
            if (isAdded)
                return StatusCode(201, new ResponseObject(ResponseType.Success.GetDisplayNameProperty(), "Объект викторины создан!"));
            return StatusCode(422, new ResponseObject(ResponseType.Failure.GetDisplayNameProperty(),
                "Не удалось создать викторину. Проверьте идентификаторы категории и подкатегорий, уникальность викторины."));
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var results = await _service.GetAll();
            if (results is not null && results.Count > 0)
            {
                var mappedResults = _mapper.Map<List<QuizViewModel>>(results);
                return StatusCode(200, mappedResults);
            }
            return StatusCode(404, new ResponseObject(ResponseType.NoResult.GetDisplayNameProperty(), "Викторины отсутствуют"));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _service.GetById(id);
            if (result is null)
                return StatusCode(404, new ResponseObject(ResponseType.NoResult.GetDisplayNameProperty(), "Викторины не существует."));
            var mappedResult = _mapper.Map<QuizViewModel>(result);
            return StatusCode(200, mappedResult);
        }
    }
}
