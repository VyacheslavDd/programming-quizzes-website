using AutoMapper;
using DProgQuizWebsite.Api.PostModels;
using ProgQuizWebsite.Api.PostModels;
using ProgQuizWebsite.Api.ViewModels;
using ProgQuizWebsite.Domain.CategoryModels;
using ProgQuizWebsite.Domain.QuizContentModels;
using ProgQuizWebsite.Domain.QuizModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgQuizWebsite.Infrastracture.Mappers
{
    public class QuizMapper : Profile
    {
        public QuizMapper()
        {
            CreateMap<LanguageCategory, CategoryViewModel>()
                .ForMember(cvm => cvm.Subcategories, m => m.MapFrom(lc =>
                lc.Subcategories.Select(sc => new SubcategoryViewModel() { Id = sc.Id, Name = sc.Name })));

            CreateMap<CategoryPostModel, LanguageCategory>();

            CreateMap<QuizSubcategory, SubcategoryViewModel>();
                //.ForMember(svm => svm.CategoryName, qz => qz.MapFrom(s => s.LanguageCategory.Name));

            CreateMap<SubcategoryPostModel, QuizSubcategory>();

            CreateMap<Quiz, QuizViewModel>()
                .ForMember(qzm => qzm.CreationDate, m => m.MapFrom(qz => qz.CreationDate.ToShortDateString()))
                .ForMember(qzm => qzm.CategoryName, m => m.MapFrom(qz => qz.LanguageCategory.Name));

            CreateMap<QuizPostModel, Quiz>()
                .ForMember(qz => qz.CreationDate, m => m.MapFrom(qpm => DateTime.UtcNow))
                .ForMember(qz => qz.Subcategories, m => m.MapFrom(qpm => new List<QuizSubcategory>()));

            CreateMap<Question, QuestionViewModel>()
                .ForMember(qvm => qvm.QuestionType, m => m.MapFrom(q => q.Type))
                .ForMember(qvm => qvm.Answers, m => m.MapFrom(q =>
                q.Answers.Select(a => new AnswerViewModel() { Id = a.Id, Name = a.Name, QuestionTitle = q.Title,
                IsCorrect = a.IsCorrect})));
            CreateMap<QuestionPostModel, Question>();

            CreateMap<Answer, AnswerViewModel>()
                .ForMember(avm => avm.QuestionTitle, m => m.MapFrom(a => a.Question.Title));
            CreateMap<AnswerPostModel, Answer>();
        }
    }
}
