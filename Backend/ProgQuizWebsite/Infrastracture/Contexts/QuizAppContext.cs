
using Microsoft.EntityFrameworkCore;
using ProgQuizWebsite.Domain.Quizzes.Models.CategoryModels;
using ProgQuizWebsite.Domain.Quizzes.Models.QuizContentModels;
using ProgQuizWebsite.Domain.Quizzes.Models.QuizModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Models;

namespace ProgQuizWebsite.Infrastracture.Contexts
{
    public class QuizAppContext : DbContext
    {
        public virtual DbSet<Quiz?> Quizzes { get; set; }
        public virtual DbSet<LanguageCategory?> LanguageCategories { get; set; }
        public virtual DbSet<QuizSubcategory?> Subcategories { get; set; }
        public virtual DbSet<Question?> Questions { get; set; }
        public virtual DbSet<Answer?> Answers { get; set; }
		public virtual DbSet<User?> Users { get; set; }
		public virtual DbSet<Role?> Roles { get; set; }

		public QuizAppContext(DbContextOptions<QuizAppContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
