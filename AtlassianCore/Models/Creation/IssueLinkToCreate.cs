using AtlassianCore.Utility;
using Newtonsoft.Json;

namespace AtlassianCore.Models.Creation
{
    [ToString]
    [JsonConverter(typeof(JsonPathConverter))]
    public class IssueLinkToCreate
    {
        [JsonProperty("type.name")]
        public string LinkType
        {
            get;
            set;
        }

        [JsonProperty("inwardIssue.key")]
        public string InwardIssueKey
        {
            get;
            set;
        }

        [JsonProperty("outwardIssue.key")]
        public string OutwardIssueKey
        {
            get;
            set;
        }
    }
}
