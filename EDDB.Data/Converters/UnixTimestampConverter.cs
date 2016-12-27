using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.TypeConversion;
using Newtonsoft.Json;

namespace EDDB.Data.Converters
{
	internal class UnixTimestampConverter : JsonConverter, ITypeConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(DateTime);
		}

		public bool CanConvertFrom(Type type)
		{
			return type == typeof(DateTime);
		}

		public bool CanConvertTo(Type type)
		{
			return type == typeof(DateTime);
		}

		public object ConvertFromString(TypeConverterOptions options, string text)
		{
			long timestamp = long.Parse(text);
			return UnixTimestampExtensions.FromUnixTimestamp(timestamp);
		}

		public string ConvertToString(TypeConverterOptions options, object value)
		{
			return ((DateTime)value).ToUnixTimestamp().ToString();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			long ts = long.Parse((string)reader.Value);
			return UnixTimestampExtensions.FromUnixTimestamp(ts);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			DateTime dt = (DateTime)value;
			writer.WriteValue(dt.ToUnixTimestamp());
		}
	}

	internal static class UnixTimestampExtensions
	{
		public static long ToUnixTimestamp(this DateTime dt)
		{
			return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Subtract(dt).Ticks / TimeSpan.TicksPerSecond;
		}

		public static DateTime FromUnixTimestamp(long ts)
		{
			return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(ts);
		}
	}
}
