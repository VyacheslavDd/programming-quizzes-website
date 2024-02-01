using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.ViewModels
{
    public class QuizViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Difficulty { get; set; }
        public string? CreationDate { get; set; }
        public string? CategoryName { get; set; }
        public List<SubcategoryViewModel?> Subcategories { get; set; }
    }
}
