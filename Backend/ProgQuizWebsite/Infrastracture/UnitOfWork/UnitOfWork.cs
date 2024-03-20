
using Core.Base.Repository;
using ProgQuizWebsite.Domain.CategoryModels;
using ProgQuizWebsite.Domain.QuizContentModels;
using ProgQuizWebsite.Domain.QuizModels;
using ProgQuizWebsite.Infrastracture.Contexts;
using ProgQuizWebsite.Infrastracture.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgQuizWebsite.Infrastracture.UnitOfWork
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
