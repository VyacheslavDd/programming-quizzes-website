using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Layer.Models;
using Business_Layer.Mappers;
using Business_Layer.Services.Implementations.MainServices;
using ProgQuizWebsite.Controllers;
using ProgQuizWebsite.Tests.Helper;
using Data_Layer.Models.CategoryModels;

namespace ProgQuizWebsite.Tests.ServicesTests
{
	[TestFixture]
	public class CategoryServiceTests
	{
		private CategoryService GetService()
		{
			var unitOfWork = TestsHelper.SetupUnitOfWork();
			return new CategoryService(unitOfWork);
		}

		[Test]
		public async Task GetAllReturnsAllElements()
		{
			var service = GetService();
			Assert.That((await service.GetAllAsync()).Count, Is.EqualTo(5));
		}

		[TestCase(new object[] { 1, "C#" })]
		[TestCase(new object[] { 2, "Python" })]
		[TestCase(new object[] { 3, "Javascript" })]
		public async Task GetByExistingIdReturnsElement(int id, string expectedValue)
		{
			var service = GetService();
			var element = await service.GetByIdAsync(id);
			Assert.That(element, Is.Not.Null);
			Assert.That(element.Name, Is.EqualTo(expectedValue));
		}

		public async Task GetByNotExistingElementReturnsNull()
		{
			var service = GetService();
			var element = await service.GetByIdAsync(6);
			Assert.That(element, Is.Null);
		}

		[Test]
		public async Task DeleteAsyncDeletesElement()
		{
			var service = GetService();
			var isDeleted = await service.DeteteAsync(3);
			var element = await service.GetByIdAsync(3);
			Assert.That(isDeleted, Is.True);
			Assert.That(element, Is.Null);
		}
	}
}
