
using Core.Base.Repository;
using ProgQuizWebsite.Domain.Quizzes.Interfaces;
using ProgQuizWebsite.Domain.Quizzes.Models.CategoryModels;
using ProgQuizWebsite.Domain.Quizzes.Models.QuizContentModels;
using ProgQuizWebsite.Domain.Quizzes.Models.QuizModels;
using ProgQuizWebsite.Infrastracture.Contexts;
using ProgQuizWebsite.Infrastracture.Quizzes.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgQuizWebsite.Infrastracture.Quizzes.UnitOfWork
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
        public IQuizRatingRepository QuizRatingRepository => new QuizRatingRepository(_context);

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
