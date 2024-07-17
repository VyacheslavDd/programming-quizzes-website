using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Converters.Enums
{
	public class IntEnumConverter<T> : JsonConverter<T> where T : Enum
	{
		public override T? ReadJson(JsonReader reader, Type objectType, T? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.Integer)
			{
				throw new JsonSerializationException("Expected integer");
			}

			int intValue = Convert.ToInt32(reader.Value);
			return (T)Enum.ToObject(typeof(T), intValue);
		}


		public override void WriteJson(JsonWriter writer, T? value, JsonSerializer serializer)
		{
			writer.WriteValue(Convert.ToInt32(value));
		}
	}
}
