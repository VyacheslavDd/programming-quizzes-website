﻿using Data_Layer.Models.CategoryModels;
using Data_Layer.Models.QuizContentModels;
using Data_Layer.Models.QuizModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Contexts
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

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
            //modelBuilder.Entity<Quiz>()
            //    .HasOne(q => q.LanguageCategory)
            //    .WithMany(c => c.Quizzes)
            //    .HasForeignKey(q => q.LanguageCategoryId);

            //modelBuilder.Entity<Question>()
            //    .HasOne(q => q.Quiz)
            //    .WithMany(qz => qz.Questions)
            //    .HasForeignKey(q => q.QuizId);

            //modelBuilder.Entity<LanguageCategory>()
            //    .HasMany(c => c.Quizzes)
            //    .WithOne(q => q.LanguageCategory)
            //    .HasForeignKey(q => q.LanguageCategoryId);

            //modelBuilder.Entity<Answer>()
            //    .HasOne(a => a.Question)
            //    .WithMany(q => q.Answers)
            //    .HasForeignKey(q => q.QuestionId);

        //    base.OnModelCreating(modelBuilder);
        //}
    }
}