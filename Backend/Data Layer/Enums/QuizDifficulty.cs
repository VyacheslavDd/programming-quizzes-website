using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Enums
{
    public enum QuizDifficulty
    {
        [Display(Name = "Легкий")]
        Easy,
        [Display(Name = "Средний")]
        Medium,
        [Display(Name = "Тяжелый")]
        Hard,
        [Display(Name = "Эксперт")]
        Expert
    }
}
