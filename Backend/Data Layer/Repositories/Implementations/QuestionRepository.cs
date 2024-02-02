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
	internal class QuestionRepository : IRepository<Question>
	{
		private readonly QuizAppContext _context;

		public QuestionRepository(QuizAppContext context)
		{
			_context = context;
		}

		public async Task AddAsync(Question? item)
		{
			await _context.Questions.AddAsync(item);
		}

		public async Task<List<Question?>> GetAllAsync()
		{
			return await _context.Questions.Include(q => q.Quiz).ToListAsync();
		}

		public async Task<Question?> GetByIdAsync(int id)
		{
			return await _context.Questions.Include(q => q.Quiz).Include(q => q.Answers)
				.FirstOrDefaultAsync(q => q.Id == id);
		}
	}
}
