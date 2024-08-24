using Core.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Emailing
{
	public static class EmailingHelper
	{
		public static string ReadSmtpPassword()
		{
			var filePath = Path.GetFullPath(Path.Combine("..", "ProgQuizWebsite", SpecialConstants.SmptPasswordFileName));
			return File.ReadAllText(filePath).Trim();
		}
	}
}
