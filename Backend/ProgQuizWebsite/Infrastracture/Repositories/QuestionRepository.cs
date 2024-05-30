using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Base.Repository;
using Microsoft.EntityFrameworkCore;
using ProgQuizWebsite.Domain.QuizContentModels;
using ProgQuizWebsite.Infrastracture.Contexts;

namespace ProgQuizWebsite.Infrastracture.Repositories
{
    internal class QuestionRepository : BaseRepository<Question>
    {
        private readonly QuizAppContext _context;

        public QuestionRepository(QuizAppContext context) : base(context, context.Questions)
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
