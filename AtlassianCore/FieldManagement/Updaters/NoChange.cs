using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtlassianCore.Models;
using AtlassianCore.Utility;

namespace AtlassianCore.FieldManagement.Updaters
{
    public class NoChange : IFieldUpdater
    {
        /// <summary>
        /// Current NLog.
        /// </summary>
        private static readonly NLog.Logger msLogger = NLog.LogManager.GetCurrentClassLogger();

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
            msLogger.Debug("-------->No change applied to " + issue.Key);

            return "";
        }

        /// <summary>
        /// Overrides ToString
        /// </summary>
        public override string ToString()
        {
            return "No change";
        }
    }
}
