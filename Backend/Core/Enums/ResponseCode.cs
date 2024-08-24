
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
        Success = 200,
        Created = 201,
        NoResult = 204,
        NotFound = 404,
        InternalServerError = 500,
        Conflict = 409,
		Unathorized = 401,
		BadRequest = 400,
        Forbidden = 403
	}
}
