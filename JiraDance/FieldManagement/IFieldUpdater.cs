using JiraDance.Models;

namespace JiraDance.FieldManagement
{
    /// <summary>
    /// This interface updates a field to a value or a computation of different values.
    /// </summary>
    public interface IFieldUpdater
    {
        /// <summary>
        /// Internal type.
        /// </summary>
        string Type
        {
            get;
        }

        /// <summary>
        /// This method updates the value according to the source values.
        /// </summary>
        /// <param name="issue">The issue to update.</param>
        /// <param name="targetField">The target field.</param>
        /// <param name="targetValue">The target value.</param>
        /// <returns>True if a modification has been done, false otherwise.</returns>
        string Update(IJiraIssue issue, string targetField, string targetValue);
    }
}
