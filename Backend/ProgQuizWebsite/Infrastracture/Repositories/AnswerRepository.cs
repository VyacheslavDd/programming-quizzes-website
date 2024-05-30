using Core.Base.Repository;
using Microsoft.EntityFrameworkCore;
using ProgQuizWebsite.Domain.QuizContentModels;
using ProgQuizWebsite.Infrastracture.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgQuizWebsite.Infrastracture.Repositories
{
    public class AnswerRepository : BaseRepository<Answer>
    {
        private readonly QuizAppContext _context;

        public AnswerRepository(QuizAppContext context) : base(context, context.Answers)
        {
            _context = context;
        }

        public override async Task<List<Answer?>> GetAllAsync()
        {
            return await _context.Answers.AsNoTracking().Include(a => a.Question).ToListAsync();
        }

        public override async Task<Answer?> GetByGuidAsync(Guid id)
        {
            return await _context.Answers.Include(a => a.Question).FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
