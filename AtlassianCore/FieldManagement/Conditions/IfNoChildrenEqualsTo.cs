using AtlassianCore.Models;
using AtlassianCore.Utility;
using System.Collections.Generic;

namespace AtlassianCore.FieldManagement.Conditions
{
    /// <summary>
    /// A condition which check if no child is equal to a value.
    /// </summary>
    public class IfNoChildrenEqualsTo : IFieldCondition
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
                    return false;
                }
            }

            return true;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return "If no child equals to " + this.Value;
        }
    }
}
