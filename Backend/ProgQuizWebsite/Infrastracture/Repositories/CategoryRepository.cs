
using Core.Base.Repository;
using Microsoft.EntityFrameworkCore;
using ProgQuizWebsite.Domain.CategoryModels;
using ProgQuizWebsite.Infrastracture.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgQuizWebsite.Infrastracture.Repositories
{
    public class CategoryRepository : BaseRepository<LanguageCategory>
    {
        private readonly QuizAppContext _context;

        public CategoryRepository(QuizAppContext context) : base(context.LanguageCategories)
        {
            _context = context;
        }

        public override async Task<List<LanguageCategory?>> GetAllAsync()
        {
            return await _context.LanguageCategories.AsNoTracking().Include(lc => lc.Subcategories).ToListAsync();
        }

        public override async Task<LanguageCategory?> GetByGuidAsync(Guid id)
        {
            return await _context.LanguageCategories.Include(lc => lc.Subcategories).ThenInclude(lc => lc.Quizzes)
                .FirstOrDefaultAsync(lc => lc.Id == id);
        }
    }
}
