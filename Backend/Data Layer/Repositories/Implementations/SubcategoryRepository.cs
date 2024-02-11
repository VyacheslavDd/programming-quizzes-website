using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Layer.Contexts;
using Data_Layer.Models.CategoryModels;
using Data_Layer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data_Layer.Repositories.Implementations
{
    public class SubcategoryRepository : BaseRepository<QuizSubcategory>
    {
        private readonly QuizAppContext _context;

        public SubcategoryRepository(QuizAppContext context) : base(context.Subcategories)
        {
            _context = context;
        }

		public override async Task<List<QuizSubcategory?>> GetAllAsync()
        {
            return await _context.Subcategories.AsNoTracking().Include(qz => qz.LanguageCategory).ToListAsync();
        }

        public override async Task<QuizSubcategory?> GetByIdAsync(int id)
        {
            return await _context.Subcategories.Include(qz => qz.LanguageCategory).Include(s => s.Quizzes)
                .FirstOrDefaultAsync(qz => qz.Id == id);
        }
    }
}
