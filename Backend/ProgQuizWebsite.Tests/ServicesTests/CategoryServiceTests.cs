using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgQuizWebsite.Services.Implementations.AdditionalServices;
using ProgQuizWebsite.Services.Implementations.MainServices;
using ProgQuizWebsite.Tests.Helper;

namespace ProgQuizWebsite.Tests.ServicesTests
{
	[TestFixture]
	public class CategoryServiceTests
	{
		private CategoryService GetService()
		{
			var unitOfWork = TestsHelper.SetupUnitOfWork();
			return new CategoryService(unitOfWork, new ValidationService(), null, null);
		}

		[Test]
		public async Task GetAllReturnsAllElements()
		{
			var service = GetService();
			Assert.That((await service.GetAllAsync()).Count, Is.EqualTo(5));
		}

		[TestCase(new object[] { "11111111-1111-1111-1111-111111111111", "C#" })]
		[TestCase(new object[] { "22222222-2222-2222-1111-222222222222", "Python" })]
		[TestCase(new object[] { "33333333-3333-3333-1111-333333333333", "Javascript" })]
		public async Task GetByExistingIdReturnsElement(string value, string expectedValue)
		{
			var guid = new Guid(value);
			var service = GetService();
			var element = await service.GetByGuidAsync(guid);
			Assert.That(element, Is.Not.Null);
			Assert.That(element.Name, Is.EqualTo(expectedValue));
		}

		public async Task GetByNotExistingElementReturnsNull()
		{
			var service = GetService();
			var element = await service.GetByGuidAsync(new Guid("66666666-6666-6666-1111-666666666666"));
			Assert.That(element, Is.Null);
		}

		[Test]
		public async Task DeleteAsyncDeletesElement()
		{
			var service = GetService();
			var guid = new Guid("33333333-3333-3333-1111-333333333333");
			await service.DeteteAsync(guid);
			var element = await service.GetByGuidAsync(guid);
			Assert.That(element, Is.Null);
		}
	}
}
