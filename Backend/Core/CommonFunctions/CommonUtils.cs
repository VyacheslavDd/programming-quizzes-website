using Core.Constants;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CommonFunctions
{
	public static class CommonUtils
	{
		public static bool BeCorrectExtension(IFormFile file)
		{
			if (file is null) return false;
			var extension = Path.GetExtension(file.FileName);
			return DataRestrictions.AllowedImageExtensions.Any(ext => extension.EndsWith(ext));
		}

		public static string GenerateUniqueSequence()
		{
			var randomData = RandomNumberGenerator.GetBytes(SpecialConstants.RNGLength);
			var sequenceString = new StringBuilder();
			foreach (var item in randomData)
				sequenceString.Append(item.ToString());
			return sequenceString.ToString();
		}
	}
}
