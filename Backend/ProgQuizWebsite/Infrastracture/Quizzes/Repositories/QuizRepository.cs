﻿
using Core.Base.Repository;
using Microsoft.EntityFrameworkCore;
using ProgQuizWebsite.Domain.Quizzes.Models.QuizModels;
using ProgQuizWebsite.Infrastracture.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgQuizWebsite.Infrastracture.Quizzes.Repositories
{


    public class QuizRepository : BaseRepository<Quiz>
    {
        private readonly QuizAppContext _context;

        public QuizRepository(QuizAppContext context) : base(context, context.Quizzes)
        {
            _context = context;
        }

        public override async Task<List<Quiz>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Quizzes.AsNoTracking().Include(qz => qz.LanguageCategory).Include(qz => qz.Subcategories)
                .ThenInclude(qz => qz.LanguageCategory).ToListAsync(cancellationToken);
        }

        public override async Task<Quiz?> GetByGuidAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Quizzes.Include(qz => qz.Questions).ThenInclude(q => q.Answers).Include(qz => qz.Subcategories)
                .ThenInclude(qz => qz.LanguageCategory)
                .FirstOrDefaultAsync(qz => qz.Id == id, cancellationToken);
        }
    }
}
