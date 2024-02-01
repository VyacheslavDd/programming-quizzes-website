
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Enums
{
    public enum QuestionType
    {
        [Display(Name = "Одиночный выбор")]
        Single = 1,
        [Display(Name = "Множественный выбор")]
        Multiple
    }
}
