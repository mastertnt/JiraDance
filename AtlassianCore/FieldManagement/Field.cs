using System.Text;

namespace AtlassianCore.FieldManagement
{
    /// <summary>
    /// This class represents a field.
    /// </summary>
    public class Field
    {
        /// <summary>
        /// Gets or sets the field name.
        /// </summary>
        public string FieldName
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
            lBuilder.Append(": " + this.FieldName);
            return lBuilder.ToString();
        }
    }
}
