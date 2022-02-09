using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JiraDance.Models
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
