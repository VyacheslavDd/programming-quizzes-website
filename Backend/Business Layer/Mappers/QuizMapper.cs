using AutoMapper;
using Data_Layer.Models.CategoryModels;
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
            CreateMap<LanguageCategory, CategoryViewModel>();
            CreateMap<CategoryPostModel, LanguageCategory>();

            CreateMap<QuizSubcategory, SubcategoryViewModel>()
                .ForMember(svm => svm.CategoryName, qz => qz.MapFrom(s => s.LanguageCategory.Name));
            CreateMap<SubcategoryPostModel, QuizSubcategory>();
        }
    }
}
