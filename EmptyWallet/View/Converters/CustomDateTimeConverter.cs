using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Globalization;

public class CustomDateTimeConverter : DateTimeConverterBase
{
    private readonly string _dateFormat;

    public CustomDateTimeConverter(string dateFormat)
    {
        _dateFormat = dateFormat;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value is DateTime dateTime)
        {
            writer.WriteValue(dateTime.ToString(_dateFormat, CultureInfo.InvariantCulture));
        }
        else
        {
            writer.WriteNull();
        }
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.String)
        {
            string dateText = reader.Value.ToString();
            if (DateTime.TryParseExact(dateText, _dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                return date;
            }
            // If parsing fails, you can return DateTime.MinValue or any other default value.
        }

        return DateTime.MinValue;
    }
}