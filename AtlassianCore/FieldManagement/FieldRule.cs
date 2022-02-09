using System.Text;

namespace AtlassianCore.FieldManagement
{
    public class FieldRule
    {
        /// <summary>
        /// Gets or sets a condition.
        /// </summary>
        public IFieldCondition Condition
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets an updater.
        /// </summary>
        public IFieldUpdater Updater
        {
            get;
            set;
        }

        /// <summary>
        /// Overrides ToString
        /// </summary>
        public override string ToString()
        {
            StringBuilder lBuilder = new StringBuilder();
            lBuilder.Append(this.Condition.ToString());
            lBuilder.Append(" then " + this.Updater.ToString());
            return lBuilder.ToString();
        }
    }
}
