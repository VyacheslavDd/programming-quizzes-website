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
	public class AnswerRepository : IRepository<Answer>
	{
		private readonly QuizAppContext _context;

		public AnswerRepository(QuizAppContext context)
		{
			_context = context;
		}

		public async Task AddAsync(Answer? answer)
		{
			await _context.Answers.AddAsync(answer);
			await _context.SaveChangesAsync();

		}

		public async Task<List<Answer?>> GetAllAsync()
		{
			return await _context.Answers.ToListAsync();
		}

		public async Task<Answer?> GetByIdAsync(int id)
		{
			return await _context.Answers.Include(a => a.Question).FirstOrDefaultAsync(a => a.Id == id);
		}
	}
}
