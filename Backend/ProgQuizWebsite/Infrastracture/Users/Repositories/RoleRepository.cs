using Core.Base.Repository;
using Microsoft.EntityFrameworkCore;
using ProgQuizWebsite.Infrastracture.Contexts;
using UserService.Domain.Interfaces;
using UserService.Domain.Models;

namespace UserService.Infrastructure.Repositories
{
	internal class RoleRepository : BaseRepository<Role>, IRoleRepository
	{
		private readonly QuizAppContext _quizAppContext;

		public RoleRepository(QuizAppContext quizAppContext) : base(quizAppContext, quizAppContext.Roles)
		{
			_quizAppContext = quizAppContext;
		}

		public async Task<Role?> GetDefaultRoleAsync()
		{
			return await _quizAppContext.Roles.FirstOrDefaultAsync(r => r.IsDefault);
		}
	}
}
