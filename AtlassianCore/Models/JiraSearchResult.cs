using Newtonsoft.Json;
using System.Collections.Generic;

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

    [ToString]
    public class JiraSearchResultSmall
    {
        [JsonProperty("issues")]
        public List<JiraIssueSmall> Issues
        {
            get;
            set;
        }
    }
}
