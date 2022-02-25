using AtlassianCore.Utility;
using Newtonsoft.Json;

namespace AtlassianCore.Models
{
    [ToString]
    [JsonConverter(typeof(JsonPathConverter))]
    public class JiraCustomField
    {
        [JsonProperty("id")]
        public string Id
        {
            get;
            set;
        }

        [JsonProperty("name")]
        public string Name
        {
            get;
            set;
        }
    }
}
