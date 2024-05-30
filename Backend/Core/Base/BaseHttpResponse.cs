using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Base
{
	public class BaseHttpResponse
	{
		public required ResponseCode ResponseCode { get; set; }
		public string ErrorMessage { get; set; }
	}
}
