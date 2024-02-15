using Data_Layer.Constants;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.CommonFunctions
{
	public static class CommonUtils
	{
		public static bool BeCorrectExtension(IFormFile file)
		{
			if (file is null) return false;
			var extension = Path.GetExtension(file.FileName);
			return DataRestrictions.AllowedImageExtensions.Any(ext => extension.EndsWith(ext));
		}
	}
}
