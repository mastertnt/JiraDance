using AtlassianCore.Utility;
using Newtonsoft.Json;

namespace AtlassianCore.Models
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class JiraIssueSmall
    {
        /// <inheritdoc/>
        [JsonProperty("id")]
        public string Id
        {
            get;
            set;
        }

        /// <inheritdoc/>
        [JsonProperty("key")]
        public string Key
        {
            get;
            set;
        }

        public override string ToString()
        {
            return this.Key;
        }
    }
}
