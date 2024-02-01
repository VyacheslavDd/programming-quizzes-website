using AutoMapper;
using Business_Layer.Extensions;
using Data_Layer.Models.CategoryModels;
using Data_Layer.Models.QuizModels;
using Data_Layer.PostModels;
using Data_Layer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Mappers
{
    public class QuizMapper : Profile
    {
        public QuizMapper()
        {
            CreateMap<LanguageCategory, CategoryViewModel>()
                .ForMember(cvm => cvm.Subcategories, m => m.MapFrom(lc =>
                lc.Subcategories.Select(sc => new SubcategoryViewModel() { Id = sc.Id, Name = sc.Name, CategoryName = sc.LanguageCategory.Name })));

            CreateMap<CategoryPostModel, LanguageCategory>();

            CreateMap<QuizSubcategory, SubcategoryViewModel>()
                .ForMember(svm => svm.CategoryName, qz => qz.MapFrom(s => s.LanguageCategory.Name));

            CreateMap<SubcategoryPostModel, QuizSubcategory>();

            CreateMap<Quiz, QuizViewModel>()
                .ForMember(qzm => qzm.Difficulty, m => m.MapFrom(qz => qz.Difficulty.GetDisplayNameProperty()))
                .ForMember(qzm => qzm.CreationDate, m => m.MapFrom(qz => qz.CreationDate.ToShortDateString()))
                .ForMember(qzm => qzm.CategoryName, m => m.MapFrom(qz => qz.LanguageCategory.Name))
                .ForMember(qzm => qzm.Subcategories,
            m => m.MapFrom(qz => qz.Subcategories.Select(sc => new SubcategoryViewModel() { Id = sc.Id, Name = sc.Name, CategoryName = sc.LanguageCategory.Name })));

            CreateMap<QuizPostModel, Quiz>()
                .ForMember(qz => qz.CreationDate, m => m.MapFrom(qpm => DateTime.UtcNow))
                .ForMember(qz => qz.Subcategories, m => m.MapFrom(qpm => new List<QuizSubcategory>()));
        }
    }
}
