using AtlassianCore.Models;
using System.Collections.Generic;

namespace AtlassianCore.FieldManagement.Conditions
{
    public class NoChild : IFieldCondition
    {
        /// <inheritdoc/>
        public string Type
        {
            get
            {
                return this.GetType().Name;
            }
        }

        /// <inheritdoc/>
        public bool IsRaised(List<IJiraIssue> children, string field)
        {
            if (children.Count == 0)
            {
                return true;
            }
            return false;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return "If the artifact has no child ";
        }
    }
}
