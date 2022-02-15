using Newtonsoft.Json;

namespace AtlassianCore.Models
{
    /// <summary>
    /// This class exposes a JIRA paragraph
    /// </summary>
    [ToString]
    public class JiraParagraph
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public JiraParagraph()
        {
            this.Type = "text";
            this.Text = "";
        }

        /// <summary>
        /// Type of the comment.
        /// </summary>
        [JsonProperty("type")]
        public string Type
        {
            get;
            set;
        }

        [JsonProperty("text")]
        public string Text
        {
            get;
            set;
        }
    }

    /// <summary>
    /// This class exposes a JIRA content
    /// </summary>
    [ToString]
    public class JiraContent
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public JiraContent()
        {
            this.Type = "paragraph";
            this.Content = new List<JiraParagraph>();
        }

        /// <summary>
        /// Type of the comment.
        /// </summary>
        [JsonProperty("type")]
        public string Type
        {
            get;
            set;
        }

        /// <summary>
        /// Content of the paragraph.
        /// </summary>
        [JsonProperty("content")]
        public List<JiraParagraph> Content
        {
            get;
            set;
        }
    }

    /// <summary>
    /// This class exposes a JIRA comment
    /// </summary>
    [ToString]
    public class JiraComment
    {
        [JsonProperty("body")]
        public string Body
        {
            get;
            set;
        }
    }
}
