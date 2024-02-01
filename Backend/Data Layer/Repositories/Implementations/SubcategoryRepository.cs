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
    public class SubcategoryRepository : ISubcategoryRepository
    {
        private readonly QuizAppContext _context;

        public SubcategoryRepository(QuizAppContext context)
        {
            _context = context;
        }

        public async Task AddSubcategory(QuizSubcategory quizSubcategory)
        {
            await _context.Subcategories.AddAsync(quizSubcategory);
            await _context.SaveChangesAsync();
        }

        public async Task<List<QuizSubcategory?>> GetAll()
        {
            return await _context.Subcategories.Include(qz => qz.LanguageCategory).ToListAsync();
        }

        public async Task<QuizSubcategory?> GetSubcategoryById(int id)
        {
            return await _context.Subcategories.Include(qz => qz.LanguageCategory)
                .Include(qz => qz.Quizzes)
                .FirstOrDefaultAsync(qz => qz.Id == id);
        }
    }
}
