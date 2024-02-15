using Data_Layer.Enums;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data_Layer.PostModels
{
	public class QuestionPostModel
	{
		[EnumDataType(typeof(QuestionType))]
		[JsonConverter(typeof(StringEnumConverter))]
		public QuestionType Type { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public string? SuccessInfo { get; set; }
		public string? FailureInfo { get; set; }
		public int QuizId { get; set; }
		public IFormFile Image { get; set; }
	}
}
