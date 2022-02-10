using AtlassianCore.Models;
using AtlassianCore.Utility;

namespace AtlassianCore.FieldManagement.Updaters
{
    public class SetValue : IFieldUpdater
    {
        /// <summary>
        /// Current NLog.
        /// </summary>
        private static readonly NLog.Logger msLogger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The value to set.
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
        /// This method updates the value according to the source values.
        /// </summary>
        /// <param name="issue">The issue to update.</param>
        /// <param name="targetField">The target field.</param>
        /// <param name="targetValue">The target value.</param>
        /// <returns>True if a modification has been done, false otherwise.</returns>
        public string Update(IJiraIssue issue, string targetField, string targetValue)
        {
            object lOldValue = issue.GetPropertyValue(targetField);
            string lOldValueStr = "null";
            if (lOldValue != null)
            {
                lOldValueStr = lOldValue.ToString();
            }
            
            if (lOldValueStr != this.Value.ToString())
            {
                issue.SetPropertyValue(targetField, Value);
                msLogger.Debug("-------->Change the value of " + issue.Key + " from " + lOldValueStr + " to " + this.Value);
            }

            return "";
        }

        /// <summary>
        /// Overrides ToString
        /// </summary>
        public override string ToString()
        {
            return "set value to " + this.Value;
        }
    }
}
