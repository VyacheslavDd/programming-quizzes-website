﻿using AutoMapper;
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
    public class QuizzesController : BaseController
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
        public async Task<IActionResult> Add(QuizPostModel model)
        {
            var mappedModel = _mapper.Map<Quiz>(model);
            var isAdded = await _service.AddAsync(mappedModel, model.SubcategoriesId);
            return ProcessAdding(isAdded, "Объект викторины создан!",
                "Не удалось создать викторину. Проверьте идентификаторы категории и подкатегорий, уникальность викторины.");
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var results = await _service.GetAllAsync();
            return ProcessItems<Quiz?, QuizViewModel>(results, _mapper, "Викторины отсутствуют");
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result is null)
                return StatusCode(404, new ResponseObject(ResponseType.NoResult.GetDisplayNameProperty(), "Викторины не существует"));
            var mappedResult = _mapper.Map<QuizViewModel>(result);
            mappedResult.Subcategories = _mapper.Map<List<SubcategoryViewModel>>(result.Subcategories);
            mappedResult.Questions = _mapper.Map<List<QuestionViewModel>>(result.Questions);
            return StatusCode(200, mappedResult);
        }
    }
}
