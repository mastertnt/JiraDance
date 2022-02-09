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
    public class JiraIssueSubTask
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
    }
}
