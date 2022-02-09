namespace JiraDance.Models
{
    /// <summary>
    /// Interface for all the issues.
    /// </summary>
    public interface IJiraIssue
    {
        /// <summary>
        /// Unique internal identifier.
        /// </summary>
        string Id
        {
            get;
            set;
        }

        /// <summary>
        /// User friendly identifier.
        /// </summary>
        string Key
        {
            get;
            set;
        }

        /// <summary>
        /// List of children.
        /// </summary>
        List<IJiraIssue> Children
        {
            get;
        }

        /// <summary>
        /// Parent node.
        /// </summary>

        IJiraIssue Parent
        {
            get;
        }

        /// <summary>
        /// Gets all issues among the same connection.
        /// </summary>
        JiraIssueDatabase Database
        {
            get;
            set;
        }
    }
}
