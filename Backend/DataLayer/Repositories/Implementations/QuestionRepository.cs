using System;
using System.Collections.Generic;
using System.Linq;
using Data_Layer.Repositories.Interfaces;
using System.Text;
using System.Threading.Tasks;
using Data_Layer.Models.QuizContentModels;
using Data_Layer.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Data_Layer.Repositories.Implementations
{
	internal class QuestionRepository : BaseRepository<Question>
	{
		private readonly QuizAppContext _context;

		public QuestionRepository(QuizAppContext context) : base(context.Questions)
		{
			_context = context;
		}

		public override async Task<List<Question?>> GetAllAsync()
		{
			return await _context.Questions.AsNoTracking().Include(q => q.Quiz).ToListAsync();
		}

		public override async Task<Question?> GetByGuidAsync(Guid id)
		{
			return await _context.Questions.Include(q => q.Quiz).Include(q => q.Answers)
				.FirstOrDefaultAsync(q => q.Id == id);
		}
	}
}
