using AtlassianCore.Utility;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AtlassianCore.Models
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class JiraIssueSmall
    {
        private static Dictionary<string, string>  msCustomFields = new Dictionary<string, string>();


        public JiraIssueSmall()
        {
            
        }

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

        /// <summary>
        /// Gets or sets the issue type.
        /// </summary>
        [JsonProperty("fields.issuetype.name")]
        public string Type
        {
            get;
            set;
        }

        [JsonProperty("Dynamic")]
        public static Dictionary<string, string> CustomFields
        {
            get
            {
                return msCustomFields;
            }            
        }

        public override string ToString()
        {
            return this.Key;
        }
    }
}
