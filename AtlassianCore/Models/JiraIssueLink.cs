using Newtonsoft.Json;

namespace JiraDance.Models
{
    [ToString]
    [JsonConverter(typeof(JsonPathConverter))]
    public class JiraIssueLink
    {
        /// <summary>
        /// Retrieves the Id of the issue.
        /// </summary>
        public string Id
        {
            get
            {
                return this.IsOutward ? this.OutwardId : this.InwardId;
            }
        }

        /// <summary>
        /// Retrieves the key of the issue.
        /// </summary>
        public string Key
        {
            get
            {
                return this.IsOutward ? this.OutwardKey : this.InwardKey;
            }
        }

        /// <summary>
        /// Retrieves the type of link.
        /// </summary>
        public string Type
        {
            get
            {
                return this.IsOutward ? this.OutwardType : this.InwardType;
            }
        }

        /// <summary>
        /// Checks if the link is out or in.
        /// </summary>
        public bool IsOutward
        {
            get
            {
                return string.IsNullOrEmpty(this.OutwardType) == false;
            }
        }

        [JsonProperty("outwardIssue.id")]
        public string OutwardId
        {
            get;
            set;
        }

        [JsonProperty("outwardIssue.key")]
        public string OutwardKey
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

        [JsonProperty("inwardIssue.id")]
        public string InwardId
        {
            get;
            set;
        }

        [JsonProperty("inwardIssue.key")]
        public string InwardKey
        {
            get;
            set;
        }

        [JsonProperty("type.inward")]
        public string InwardType
        {
            get;
            set;
        }
    }
}
