namespace JiraDance
{
    /// <summary>
    /// Stores the technical settings.
    /// </summary>
    internal class TechSettings
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public TechSettings()
        {
            this.ProjectKeys = new List<string>();
        }

        /// <summary>
        /// API username.
        /// </summary>
        public string Username
        {
            get;
            set;
        }

        /// <summary>
        /// API password.
        /// </summary>
        public string Password
        {
            get;
            set;
        }

        /// <summary>
        /// URL endpoint e.g : https://xxx.atlassian.net/rest
        /// </summary>
        public string EndPoint
        {
            get;
            set;
        }

        /// <summary>
        /// Debug response path. If null, no debug is done.
        /// </summary>
        public string ResponseDebugPath
        {
            get;
            set;
        }

        /// <summary>
        /// This flag is used to log only the actions (nothing is done on the server).
        /// </summary>
        public bool LogOnlyAction
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the project keys where to retrieve the issues.
        /// </summary>
        public List<string> ProjectKeys
        {
            get;
            set;
        }
    }
}
