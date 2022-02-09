using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OfficeOpenXml.Table.PivotTable;

namespace JiraDance.Models
{
    [ToString]
    [JsonConverter(typeof(JsonPathConverter))]
    public class JiraIssue : IJiraIssue
    {
        private string mParentKey = null;


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

        /// <inheritdoc/>
        public IJiraIssue Parent
        {
            get
            {
                if (this.mParentKey == null)
                {
                    this.mParentKey = this.Issuelinks.FirstOrDefault(link => link.IsOutward && link.Type == "relates to")?.Key ?? string.Empty;
                }

                if (string.IsNullOrWhiteSpace(this.mParentKey))
                {
                    return null;
                }
                    
                return this.Database.Issues.FirstOrDefault(parent => parent.Key == this.mParentKey);
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

    }
}
