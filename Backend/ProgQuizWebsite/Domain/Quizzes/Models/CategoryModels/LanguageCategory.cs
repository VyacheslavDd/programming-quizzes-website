﻿using ProgQuizWebsite.Domain.Quizzes.Models.QuizModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgQuizWebsite.Domain.Quizzes.Models.CategoryModels
{
    [Table("Categories")]
    public class LanguageCategory
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public List<QuizSubcategory> Subcategories { get; set; }
        public List<Quiz?> Quizzes { get; set; }
    }
}
