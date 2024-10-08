﻿
using Core.Base.Repository;
using Microsoft.EntityFrameworkCore;
using ProgQuizWebsite.Domain.Quizzes.Models.CategoryModels;
using ProgQuizWebsite.Infrastracture.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgQuizWebsite.Infrastracture.Quizzes.Repositories
{
    public class CategoryRepository : BaseRepository<LanguageCategory>
    {
        private readonly QuizAppContext _context;

        public CategoryRepository(QuizAppContext context) : base(context, context.LanguageCategories)
        {
            _context = context;
        }

        public override async Task<List<LanguageCategory?>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.LanguageCategories.AsNoTracking().Include(lc => lc.Subcategories).ToListAsync(cancellationToken);
        }

        public override async Task<LanguageCategory?> GetByGuidAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.LanguageCategories.Include(lc => lc.Subcategories).ThenInclude(lc => lc.Quizzes)
                .FirstOrDefaultAsync(lc => lc.Id == id, cancellationToken);
        }
    }
}
