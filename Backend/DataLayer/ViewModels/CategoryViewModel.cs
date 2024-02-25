using Data_Layer.Models.CategoryModels;

namespace Data_Layer.ViewModels;

    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public List<SubcategoryViewModel?> Subcategories { get; set; }
        public List<QuizViewModel?> Quizzes { get; set; }
    }
