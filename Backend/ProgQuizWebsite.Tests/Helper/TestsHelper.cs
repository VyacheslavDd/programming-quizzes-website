using Data_Layer.Contexts;
using Data_Layer.Models.CategoryModels;
using Data_Layer.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgQuizWebsite.Tests.Helper
{
	public static class TestsHelper
	{
		public static IUnitOfWork SetupUnitOfWork()
		{
			var context = SetupContext();
			return new UnitOfWork(context);
		}
		private static QuizAppContext SetupContext()
		{
			var entities = GetLanguageCategories();
			var optionsBuilder = new DbContextOptionsBuilder<QuizAppContext>().Options;
			var context = new Mock<QuizAppContext>(optionsBuilder);
			context.Setup(context => context.LanguageCategories).ReturnsDbSet(entities);
			context.Setup(context => context.LanguageCategories.Remove(It.IsAny<LanguageCategory>()))
				.Callback<LanguageCategory>(category => entities.Remove(category));
			return context.Object;
		}

		private static List<LanguageCategory> GetLanguageCategories()
		{
			return new List<LanguageCategory>
			{
				new LanguageCategory() {Id = 1, Name = "C#" },
				new LanguageCategory() {Id = 2, Name = "Python" },
				new LanguageCategory() {Id = 3, Name = "Javascript" },
				new LanguageCategory() {Id = 4, Name = "C++" },
				new LanguageCategory() {Id = 5, Name = "Typescript" },
			};
		}
	}
}
