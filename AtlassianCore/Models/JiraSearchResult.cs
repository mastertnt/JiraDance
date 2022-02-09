using Newtonsoft.Json;

namespace AtlassianCore.Models
{
    [ToString]
    public class JiraSearchResult
    {
        [JsonProperty("issues")]
        public List<JiraIssue> Issues
        {
            get;
            set;
        }
    }
}
