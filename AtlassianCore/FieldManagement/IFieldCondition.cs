using AtlassianCore.Models;
using System.Collections.Generic;

namespace AtlassianCore.FieldManagement
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
        /// <param name="children">The list of issues.</param>
        /// <param name="field">The field to check.</param>
        /// <returns>True if the condition is raised, false otherwise</returns>
        bool IsRaised(List<IJiraIssue> children, string field);
    }
}
