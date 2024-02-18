using Data_Layer.Contexts;
using Data_Layer.Models.QuizContentModels;
using Data_Layer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Repositories.Implementations
{
	public class AnswerRepository : BaseRepository<Answer>
	{
		private readonly QuizAppContext _context;

		public AnswerRepository(QuizAppContext context) : base(context.Answers)
		{
			_context = context;
		}

		public override async Task<List<Answer?>> GetAllAsync()
		{
			return await _context.Answers.AsNoTracking().Include(a => a.Question).ToListAsync();
		}

		public override async Task<Answer?> GetByIdAsync(int id)
		{
			return await _context.Answers.Include(a => a.Question).FirstOrDefaultAsync(a => a.Id == id);
		}
	}
}
