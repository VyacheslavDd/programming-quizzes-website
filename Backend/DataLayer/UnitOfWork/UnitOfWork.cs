using Data_Layer.Contexts;
using Data_Layer.Models.CategoryModels;
using Data_Layer.Models.QuizContentModels;
using Data_Layer.Models.QuizModels;
using Data_Layer.Repositories.Implementations;
using Data_Layer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly QuizAppContext _context;

        public UnitOfWork(QuizAppContext context)
        {
            _context = context;
        }

        public IRepository<LanguageCategory> CategoryRepository => new CategoryRepository(_context);
        public IRepository<QuizSubcategory> SubcategoryRepository => new SubcategoryRepository(_context);
        public IRepository<Quiz> QuizRepository => new QuizRepository(_context);
        public IRepository<Question> QuestionRepository => new QuestionRepository(_context);
        public IRepository<Answer> AnswerRepository => new AnswerRepository(_context);

		public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
