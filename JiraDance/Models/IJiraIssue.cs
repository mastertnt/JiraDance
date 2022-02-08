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
        public string Id
        {
            get;
            set;
        }

        /// <summary>
        /// User friendly identifier.
        /// </summary>
        public string Key
        {
            get;
            set;
        }
    }
}
