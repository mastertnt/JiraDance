namespace JiraDance.FieldManagement.Conditions
{
    public class NoChild : IFieldCondition
    {
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
            if (pSourceValues.Count == 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Overrides ToString
        /// </summary>
        public override string ToString()
        {
            return "If the artifact has no child ";
        }
    }
}
