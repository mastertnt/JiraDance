using AtlassianCore.Utility;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace AtlassianCore.Models
{
    [ToString]
    [JsonConverter(typeof(JsonPathConverter))]
    public class JiraIssue : IJiraIssue
    {
        private bool mSearchParent = false;

        private IJiraIssue mParent = null;


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

        /// <inheritdoc/>
        public List<IJiraIssue> Children
        {
            get
            {
                return this.Database.Issues.Where(issue => issue.Parent != null && issue.Parent.Id == this.Id).ToList();
            }
        }

        public string ParentId
        {
            get
            {
                if (this.Parent != null)
                {
                    return this.Parent.Id;
                }

                return "NoParent";
            }
        }

        /// <inheritdoc/>
        public IJiraIssue Parent
        {
            get
            {
                if (this.mSearchParent == false)
                {
                    this.mSearchParent = true;

                    string parentKey = this.Issuelinks.FirstOrDefault(link => link.IsOutward && link.Type == "relates to")?.Key;
                    if (string.IsNullOrWhiteSpace(parentKey))
                    {
                        parentKey = this.SubTaskParentKey;
                    }

                    this.mParent = this.Database.Issues.FirstOrDefault(parent => parent.Key == parentKey);
                }

                return this.mParent;
            }
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
        [JsonProperty("priority.id")]
        public string Priority
        {
            get;
            set;
        }

        /// <inheritdoc/>
        public JiraIssueDatabase Database
        {
            get;
            set;
        }

        /// <summary>
        /// Get all issue links.
        /// </summary>
        [JsonProperty("fields.issuelinks")]
        public List<JiraIssueLink> Issuelinks
        {
            get;
            set;
        }

        /// <summary>
        /// Gets all sub-tasks.
        /// </summary>
        [JsonProperty("fields.sub-tasks")]
        public List<JiraIssueSubTask> SubTasks
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the status.
        /// </summary>
        [JsonProperty("fields.status.name")]
        public string Status
        {
            get;
            set;
        }

        /// <summary>
        /// When an issue is a sub-task, the parent id is written.
        /// </summary>
        [JsonProperty("fields.parent.key")]
        public string SubTaskParentKey
        {
            get;
            set;
        }
    }
}
