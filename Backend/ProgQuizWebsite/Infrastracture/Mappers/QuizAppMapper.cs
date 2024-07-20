using AutoMapper;
using ProgQuizWebsite.Api.Notifications.PostModels;
using ProgQuizWebsite.Api.Notifications.ResponseModels;
using ProgQuizWebsite.Api.Quizzes.PostModels;
using ProgQuizWebsite.Api.Quizzes.ViewModels;
using ProgQuizWebsite.Domain.Notifications.Models;
using ProgQuizWebsite.Domain.Quizzes.Models.CategoryModels;
using ProgQuizWebsite.Domain.Quizzes.Models.QuizContentModels;
using ProgQuizWebsite.Domain.Quizzes.Models.QuizModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Api.PostModels.Auth;
using UserService.Api.PostModels.Roles;
using UserService.Api.PostModels.Users;
using UserService.Api.ResponseModels.Roles;
using UserService.Api.ResponseModels.Users;
using UserService.Domain.Models;

namespace ProgQuizWebsite.Infrastracture.Mappers
{
    public class QuizAppMapper : Profile
    {
        public QuizAppMapper()
        {
			MapQuizzes();
			MapUsers();
			MapNotifications();
        }

        public void MapQuizzes()
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
				q.Answers.Select(a => new AnswerViewModel()
				{
					Id = a.Id,
					Name = a.Name,
					QuestionTitle = q.Title,
					IsCorrect = a.IsCorrect
				})));
			CreateMap<QuestionPostModel, Question>();

			CreateMap<Answer, AnswerViewModel>()
				.ForMember(avm => avm.QuestionTitle, m => m.MapFrom(a => a.Question.Title));
			CreateMap<AnswerPostModel, Answer>();
		}

        public void MapUsers()
        {
			CreateMap<User, UserResponse>();
			CreateMap<RegistrationModel, User>()
				.ForMember(user => user.PasswordHash, opt => opt.MapFrom(model =>
				BCrypt.Net.BCrypt.HashPassword(model.Password)));
			CreateMap<AuthenticationModel, User>()
				.ForMember(user => user.Email, opt => opt.MapFrom(model =>
				model.LoginOrEmail.Contains('@') ? model.LoginOrEmail : ""))
				.ForMember(user => user.Login, opt => opt.MapFrom(model =>
				model.LoginOrEmail.Contains('@') ? "" : model.LoginOrEmail));
			CreateMap<UpdateUserModel, User>();
			CreateMap<Role, RoleResponse>();
			CreateMap<RolePostModel, Role>();
			CreateMap<RoleUpdateModel, Role>();
		}

		public void MapNotifications()
		{
			CreateMap<NotificationPostModel, Notification>();
			CreateMap<Notification, NotificationResponseModel>();
		}
    }
}
