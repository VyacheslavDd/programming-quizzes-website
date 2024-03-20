
using Microsoft.EntityFrameworkCore;
using ProgQuizWebsite.Domain.CategoryModels;
using ProgQuizWebsite.Domain.QuizContentModels;
using ProgQuizWebsite.Domain.QuizModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgQuizWebsite.Infrastracture.Contexts
{
    public class QuizAppContext : DbContext
    {
        public virtual DbSet<Quiz?> Quizzes { get; set; }
        public virtual DbSet<LanguageCategory?> LanguageCategories { get; set; }
        public virtual DbSet<QuizSubcategory?> Subcategories { get; set; }
        public virtual DbSet<Question?> Questions { get; set; }
        public virtual DbSet<Answer?> Answers { get; set; }

        public QuizAppContext(DbContextOptions<QuizAppContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			base.OnModelCreating(modelBuilder);
        }
    }
}
