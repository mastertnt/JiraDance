using System.Reflection;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace JiraDance
{
    /// <summary>
    /// This class is used to retrieve nested properties.
    /// </summary>
    class JsonPathConverter : JsonConverter
    {
        /// <inheritdoc/>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            object targetObj = Activator.CreateInstance(objectType);

            foreach (PropertyInfo prop in objectType.GetProperties().Where(p => p.CanRead && p.CanWrite))
            {
                JsonPropertyAttribute att = prop.GetCustomAttributes(true).OfType<JsonPropertyAttribute>().FirstOrDefault()!;

                string jsonPath = att.PropertyName;

                if (serializer.ContractResolver is DefaultContractResolver)
                {
                    var resolver = (DefaultContractResolver)serializer.ContractResolver;
                    jsonPath = resolver.GetResolvedPropertyName(jsonPath);
                }

                if (!Regex.IsMatch(jsonPath, @"^[a-zA-Z0-9_.-]+$"))
                {
                    throw new InvalidOperationException($"JProperties of JsonPathConverter can have only letters, numbers, underscores, hiffens and dots but name was ${jsonPath}."); // Array operations not permitted
                }

                JToken token = jo.SelectToken(jsonPath);
                if (token != null && token.Type != JTokenType.Null)
                {
                    object value = token.ToObject(prop.PropertyType, serializer);
                    prop.SetValue(targetObj, value, null);
                }
            }
            return targetObj;
        }

        /// <inheritdoc/>
        public override bool CanConvert(Type objectType)
        {
            // CanConvert is not called when [JsonConverter] attribute is used
            return false;
        }

        /// <inheritdoc/>
        public override bool CanWrite
        {
            get
            {
                return false;
            }
        }

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
