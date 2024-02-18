using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Enums
{
    public enum ResponseType
    {
        [Display(Name = "Success")]
        Success = 200,
        [Display(Name = "No Result")]
        NoResult = 204,
        [Display(Name = "Failure")]
        Failure = 404
    }
}
