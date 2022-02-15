﻿using AtlassianCore.Utility;
using Newtonsoft.Json;

namespace AtlassianCore.Models
{
    [ToString]
    [JsonConverter(typeof(JsonPathConverter))]
    public class IssueToCreate
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Key
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [JsonProperty("fields.summary")]
        public string Summary
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

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        [JsonProperty("fields.priority.name")]
        public string Priority
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the project key.
        /// </summary>
        [JsonProperty("fields.project.key")]
        public string ProjectKey
        {
            get;
            set;
        }

        [JsonProperty("fields.description")]
        public string Description
        {
            get;
            set;
        }

        [JsonProperty("fields.parent.key")]
        public string ParentKey
        {
            get;
            set;
        }
    };
}
