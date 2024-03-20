
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using ProgQuizWebsite.Domain.CategoryModels;
using ProgQuizWebsite.Infrastracture.Contexts;
using ProgQuizWebsite.Infrastracture.UnitOfWork;
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
				new LanguageCategory() {Id = new Guid("11111111-1111-1111-1111-111111111111"), Name = "C#" },
				new LanguageCategory() {Id = new Guid("22222222-2222-2222-1111-222222222222"), Name = "Python" },
				new LanguageCategory() {Id = new Guid("33333333-3333-3333-1111-333333333333"), Name = "Javascript" },
				new LanguageCategory() {Id = new Guid("44444444-4444-4444-1111-444444444444"), Name = "C++" },
				new LanguageCategory() {Id = new Guid("55555555-5555-5555-1111-555555555555"), Name = "Typescript" },
			};
		}
	}
}
