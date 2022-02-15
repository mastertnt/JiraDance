using Newtonsoft.Json;

namespace AtlassianCore.Models.Bitbucket
{
    [ToString]
    public class PullRequest
    {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        [JsonProperty("id")]
        public string Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [JsonProperty("title")]
        public string Title
        {
            get;
            set;
        }

        [JsonProperty("author")]
        public string Author
        {
            get;
            set;
        }

        [JsonProperty("comment_count")]
        public int CommentCount
        {
            get;
            set;
        }

        [JsonProperty("task_count")]
        public int TaskCount
        {
            get;
            set;
        }

        [JsonProperty("created_on")]
        public string CreationDate
        {
            get;
            set;
        }

        [JsonProperty("update_on")]
        public string UpdateaDate
        {
            get;
            set;
        }

        [JsonProperty("state")]
        public string State
        {
            get;
            set;
        }

    }
}
