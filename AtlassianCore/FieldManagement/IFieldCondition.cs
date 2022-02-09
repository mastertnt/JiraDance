namespace JiraDance.FieldManagement
{
    /// <summary>
    /// This interface checks if a field must be updated according to source values.
    /// </summary>
    public interface IFieldCondition
    {
        /// <summary>
        /// Internal type.
        /// </summary>
        string Type
        {
            get;
        }

        /// <summary>
        /// This method return true if the condition is raised according to the source values.
        /// </summary>
        /// <param name="pSourceValues">The source values</param>
        /// <returns>True if the condition is raised, false otherwise</returns>
        bool IsRaised(List<object> pSourceValues);
    }
}
