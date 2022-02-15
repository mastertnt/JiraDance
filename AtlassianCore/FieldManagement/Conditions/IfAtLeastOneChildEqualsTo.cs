using AtlassianCore.Models;
using AtlassianCore.Utility;
using System.Collections.Generic;

namespace AtlassianCore.FieldManagement.Conditions
{
    /// <summary>
    /// A condition which check if at least one child is equal to a value.
    /// </summary>
    public class IfAtLeastOneChildEqualsTo : IFieldCondition
    {
        /// <summary>
        /// The value to check.
        /// </summary>
        public object Value
        {
            get;
            set;
        }

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
            foreach (var child in children)
            {
                if (child.GetPropertyValue(field).ToString() == this.Value.ToString())
                {
                    return true;
                }
            }

            return false;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return "If at least one child equals to " + this.Value;
        }
    }
}
