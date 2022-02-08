using Newtonsoft.Json;

namespace JiraDance.Models
{
    [ToString]
    [JsonConverter(typeof(JsonPathConverter))]
    public class JiraIssueLink : IJiraIssue
    {
        [JsonProperty("outwardIssue.id")]
        public string Id
        {
            get;
            set;
        }

        [JsonProperty("outwardIssue.key")]
        public string Key
        {
            get;
            set;
        }

        [JsonProperty("type.outward")]
        public string OutwardType
        {
            get;
            set;
        }
    }
}
