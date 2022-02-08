using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JiraDance.Models
{
    [ToString]
    [JsonConverter(typeof(JsonPathConverter))]
    public class JiraIssue
    {
        [JsonProperty("id")]
        public string Id
        {
            get;
            set;
        }

        [JsonProperty("key")]
        public string Key
        {
            get;
            set;
        }

        [JsonProperty("fields.issuelinks")]
        public List<JiraIssueLink> Issuelinks
        {
            get;
            set;
        }

        [JsonProperty("fields.sub-tasks")]
        public List<JiraIssueSub> SubTasks
        {
            get;
            set;
        }

        [JsonProperty("status.name")]
        public string Status
        {
            get;
            set;
        }
    }
}
