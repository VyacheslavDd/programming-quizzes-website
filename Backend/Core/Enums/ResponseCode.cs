using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums
{
    public enum ResponseCode
    {
        [Display(Name = "Success")]
        Success = 200,
        [Display(Name = "Created")]
        Created = 201,
        [Display(Name = "No Result")]
        NoResult = 204,
        [Display(Name = "Not Found")]
        NotFound = 404,
        [Display(Name = "Internal Server Error")]
        InternalServerError = 500,
        [Display(Name = "Conflict")]
        Conflict = 409,
		[Display(Name = "Unathorized")]
		Unathorized = 401,
	}
}
