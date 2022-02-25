using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace AtlassianCore.Utility
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
                JsonPropertyAttribute att = prop.GetCustomAttributes(true).OfType<JsonPropertyAttribute>().FirstOrDefault();
                if (att != null)
                {
                    if (att.PropertyName == "Dynamic")
                    {
                        //Dictionary<string, string> value = prop.GetValue(existingValue);
                        //string jsonPath = att.PropertyName;

                        Dictionary<string, string> dynamicProperties = prop.GetValue(targetObj) as Dictionary<string, string>;
                        if (dynamicProperties != null)
                        {
                            foreach (var dynamicProperty in dynamicProperties)
                            {
                                JToken token = jo.SelectToken(dynamicProperty.Key);
                                if (token != null && token.Type != JTokenType.Null)
                                {
                                    object value = token.ToObject(prop.PropertyType, serializer);
                                    dynamicProperties[dynamicProperty.Key] = value.ToString();
                                }
                            }
                        }                                                 
                    }
                    else
                    {
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
                return true;
            }
        }

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var properties = value.GetType().GetRuntimeProperties().Where(p => p.CanRead && p.CanWrite);
            JObject main = new JObject();
            foreach (PropertyInfo prop in properties)
            {
                JsonPropertyAttribute att = prop.GetCustomAttributes(true)
                    .OfType<JsonPropertyAttribute>()
                    .FirstOrDefault();

                string jsonPath = att != null ? att.PropertyName : prop.Name;

                if (serializer.ContractResolver is DefaultContractResolver)
                {
                    var resolver = (DefaultContractResolver)serializer.ContractResolver;
                    jsonPath = resolver.GetResolvedPropertyName(jsonPath);
                }

                if (jsonPath == "Dynamic")
                {
                    Dictionary<string, string> dynamicProperties = prop.GetValue(value) as Dictionary<string, string>;
                    foreach (var dynamicProperty in dynamicProperties)
                    {
                        var nesting = dynamicProperty.Key.Split('.');
                        JObject lastLevel = main;

                        for (int i = 0; i < nesting.Length; i++)
                        {
                            if (i == nesting.Length - 1)
                            {
                                lastLevel[nesting[i]] = new JValue(dynamicProperty.Value);
                            }
                            else
                            {
                                if (lastLevel[nesting[i]] == null)
                                {
                                    lastLevel[nesting[i]] = new JObject();
                                }

                                lastLevel = (JObject)lastLevel[nesting[i]];
                            }
                        }
                    }                    
                }
                else
                {
                    var nesting = jsonPath.Split('.');
                    JObject lastLevel = main;

                    for (int i = 0; i < nesting.Length; i++)
                    {
                        if (i == nesting.Length - 1)
                        {
                            lastLevel[nesting[i]] = new JValue(prop.GetValue(value));
                        }
                        else
                        {
                            if (lastLevel[nesting[i]] == null)
                            {
                                lastLevel[nesting[i]] = new JObject();
                            }

                            lastLevel = (JObject)lastLevel[nesting[i]];
                        }
                    }
                }                
            }

            serializer.Serialize(writer, main);
        }
    }
}

