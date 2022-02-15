using AtlassianCore.Models;
using System.Collections.Generic;

namespace AtlassianCore.FieldManagement.Conditions
{
    /// <summary>
    /// This class represents a condition always true.
    /// </summary>
    public class NoCondition : IFieldCondition
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
            return true;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return "";
        }
    }
}
