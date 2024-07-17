using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Base.Repository;
using Microsoft.EntityFrameworkCore;
using ProgQuizWebsite.Domain.Quizzes.Models.CategoryModels;
using ProgQuizWebsite.Infrastracture.Contexts;

namespace ProgQuizWebsite.Infrastracture.Quizzes.Repositories
{
    public class SubcategoryRepository : BaseRepository<QuizSubcategory>
    {
        private readonly QuizAppContext _context;

        public SubcategoryRepository(QuizAppContext context) : base(context, context.Subcategories)
        {
            _context = context;
        }

        public override async Task<List<QuizSubcategory?>> GetAllAsync()
        {
            return await _context.Subcategories.AsNoTracking().Include(qz => qz.LanguageCategory).ToListAsync();
        }

        public override async Task<QuizSubcategory?> GetByGuidAsync(Guid id)
        {
            return await _context.Subcategories.Include(qz => qz.LanguageCategory).Include(s => s.Quizzes)
                .FirstOrDefaultAsync(qz => qz.Id == id);
        }
    }
}
