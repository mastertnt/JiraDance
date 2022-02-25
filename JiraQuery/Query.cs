using System.ComponentModel;

namespace JiraQuery
{
    public class Query
    {
        [ReadOnly(true)]
        public string Name
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public string Description
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public string JQL
        {
            get;
            set;
        }
    }
}
