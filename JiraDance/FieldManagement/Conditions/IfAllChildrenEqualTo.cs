namespace JiraDance.FieldManagement.Conditions
{
    /// <summary>
    /// A condition which check if all children is equal to a value.
    /// </summary>
    public class IfAllChildrenEqualTo : IFieldCondition
    {
        /// <summary>
        /// The value to check.
        /// </summary>
        public object Value
        {
            get;
            set;
        }

        /// <summary>
        /// Internal type.
        /// </summary>
        public string Type
        {
            get
            {
                return this.GetType().Name;
            }
        }

        /// <summary>
        /// This method return true if the condition is raised according to the source values.
        /// </summary>
        /// <param name="pSourceValues">The list of values.</param>
        /// <returns>True if the condition is raised.</returns>
        public bool IsRaised(List<object> pSourceValues)
        {
            foreach (var lSourceValue in pSourceValues)
            {
                string lSourceValueStr = "null";
                if (lSourceValue != null)
                {
                    lSourceValueStr = lSourceValue.ToString();
                }

                if (lSourceValueStr != this.Value.ToString())
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Overrides ToString
        /// </summary>
        public override string ToString()
        {
            return "If all children equal to " + this.Value;
        }
    }
}
