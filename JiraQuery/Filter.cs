using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraQuery
{
    public class Filter
    {

        public Filter()
        {
            this.Parameters = new List<FilterParameter>();
            this.Queries = new List<Query>();
        }

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

        public List<FilterParameter> Parameters
        {
            get;
            set;
        }

        public List<Query> Queries
        {
            get;
            set;
        }
    }
}
