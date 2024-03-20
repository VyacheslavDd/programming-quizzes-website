
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums
{
    public enum QuestionType
    {
        /// <summary>
        /// Одиночный выбор
        /// </summary>
        [Display(Name = "Одиночный выбор")]
        Single = 1,
        /// <summary>
        /// Множественный выбор
        /// </summary>
        [Display(Name = "Множественный выбор")]
        Multiple
    }
}
