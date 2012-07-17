using System;
using System.Collections;
using Newtonsoft.Json;

namespace WebApi.Hal.JsonConverters
{
    public class ResourceListConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var list = (Resource)value;

            list.Links.Add(new Link
            {
                Rel = "self",
                Href = list.Href
            });

            writer.WriteStartObject();
            writer.WritePropertyName("_links");
            serializer.Serialize(writer, list.Links);

            writer.WritePropertyName("_embedded");
            writer.WriteStartObject();
            writer.WritePropertyName(list.Rel);
            writer.WriteStartArray();
            foreach (Resource halResource in (IEnumerable)value)
            {
                serializer.Serialize(writer, halResource);
            }
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return reader.Value;
        }

        public override bool CanConvert(Type objectType)
        {
            return IsResource(objectType) && IsResourceList(objectType);
        }

        static bool IsResourceList(Type objectType)
        {
            return typeof(IResourceList).IsAssignableFrom(objectType);
        }

        static bool IsResource(Type objectType)
        {
            return typeof(Resource).IsAssignableFrom(objectType);
        }
    }
}