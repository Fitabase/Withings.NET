﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Withings.API.Portable
{
    internal class EmptyDateToMinDateConverter : Newtonsoft.Json.JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            DateTime date = (DateTime)value;
            if (date == DateTime.MinValue)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteValue(date);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (CanConvert(objectType))
            {
                if (string.IsNullOrWhiteSpace(reader.Value.ToString()))
                {
                    return DateTime.MinValue;
                }

                return DateTime.Parse(reader.Value.ToString());
            }
            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(DateTime));
        }
    }
}
