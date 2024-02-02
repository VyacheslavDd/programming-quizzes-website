﻿// <auto-generated />
using System;
using Data_Layer.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ProgQuizWebsite.Migrations
{
    [DbContext(typeof(QuizAppContext))]
    [Migration("20240202214608_answer-model-update")]
    partial class answermodelupdate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Data_Layer.Models.CategoryModels.LanguageCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Data_Layer.Models.CategoryModels.QuizSubcategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("LanguageCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LanguageCategoryId");

                    b.ToTable("Subcategories");
                });

            modelBuilder.Entity("Data_Layer.Models.QuizContentModels.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("Data_Layer.Models.QuizContentModels.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FailureInfo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuizId")
                        .HasColumnType("int");

                    b.Property<string>("SuccessInfo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("QuizId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Data_Layer.Models.QuizModels.Quiz", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Difficulty")
                        .HasColumnType("int");

                    b.Property<int>("LanguageCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LanguageCategoryId");

                    b.ToTable("Quizzes");
                });

            modelBuilder.Entity("QuizQuizSubcategory", b =>
                {
                    b.Property<int>("QuizzesId")
                        .HasColumnType("int");

                    b.Property<int>("SubcategoriesId")
                        .HasColumnType("int");

                    b.HasKey("QuizzesId", "SubcategoriesId");

                    b.HasIndex("SubcategoriesId");

                    b.ToTable("QuizQuizSubcategory");
                });

            modelBuilder.Entity("Data_Layer.Models.CategoryModels.QuizSubcategory", b =>
                {
                    b.HasOne("Data_Layer.Models.CategoryModels.LanguageCategory", "LanguageCategory")
                        .WithMany("Subcategories")
                        .HasForeignKey("LanguageCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LanguageCategory");
                });

            modelBuilder.Entity("Data_Layer.Models.QuizContentModels.Answer", b =>
                {
                    b.HasOne("Data_Layer.Models.QuizContentModels.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("Data_Layer.Models.QuizContentModels.Question", b =>
                {
                    b.HasOne("Data_Layer.Models.QuizModels.Quiz", "Quiz")
                        .WithMany("Questions")
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Quiz");
                });

            modelBuilder.Entity("Data_Layer.Models.QuizModels.Quiz", b =>
                {
                    b.HasOne("Data_Layer.Models.CategoryModels.LanguageCategory", "LanguageCategory")
                        .WithMany("Quizzes")
                        .HasForeignKey("LanguageCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LanguageCategory");
                });

            modelBuilder.Entity("QuizQuizSubcategory", b =>
                {
                    b.HasOne("Data_Layer.Models.QuizModels.Quiz", null)
                        .WithMany()
                        .HasForeignKey("QuizzesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data_Layer.Models.CategoryModels.QuizSubcategory", null)
                        .WithMany()
                        .HasForeignKey("SubcategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data_Layer.Models.CategoryModels.LanguageCategory", b =>
                {
                    b.Navigation("Quizzes");

                    b.Navigation("Subcategories");
                });

            modelBuilder.Entity("Data_Layer.Models.QuizContentModels.Question", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("Data_Layer.Models.QuizModels.Quiz", b =>
                {
                    b.Navigation("Questions");
                });
#pragma warning restore 612, 618
        }
    }
}
