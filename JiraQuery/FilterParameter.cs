using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraQuery
{
    public class FilterParameter
    {
        [ReadOnly(true)]
        public string Name
        {
            get;
            set;
        }

        public string Value
        {
            get;
            set;
        }
    }
}
