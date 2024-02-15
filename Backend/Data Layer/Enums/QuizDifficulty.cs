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
        /// <summary>
        /// Лёгкая
        /// </summary>
        [Display(Name = "Легкая")]
        Easy = 1,
		/// <summary>
		/// Средняя
		/// </summary>
		[Display(Name = "Средняя")]
        Medium,
		/// <summary>
		/// Тяжелая
		/// </summary>
		[Display(Name = "Тяжелая")]
        Hard,
		/// <summary>
		/// Эксперт
		/// </summary>
		[Display(Name = "Эксперт")]
        Expert
    }
}
