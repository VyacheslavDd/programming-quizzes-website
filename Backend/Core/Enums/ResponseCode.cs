
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Core.Converters.Enums;

namespace Core.Enums
{
    [JsonConverter(typeof(IntEnumConverter<ResponseCode>))]
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
		[Display(Name = "Bad Request")]
		BadRequest = 400,
	}
}
