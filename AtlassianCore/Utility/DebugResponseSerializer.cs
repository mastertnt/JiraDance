using RestEase;

namespace AtlassianCore.Utility
{
    /// <summary>
    /// This class is used to debug the response from rest api.
    /// </summary>
    public class DebugResponseDeserializer : JsonResponseDeserializer
    {
        /// <summary>
        /// Defines th debug path.
        /// </summary>
        public string DebugPath
        {
            get;
            set;
        }

        /// <summary>
        /// Defines the response index.
        /// </summary>
        public int ResponseIndex
        {
            get;
            set;
        }

        /// <inheritdoc/>
        public override T Deserialize<T>(string? content, HttpResponseMessage response, ResponseDeserializerInfo info)
        {
            if (Directory.Exists(this.DebugPath))
            {
                File.WriteAllText(this.DebugPath  + @"\response_" + this.ResponseIndex + ".json", content);
                this.ResponseIndex++;
            }
            
            return base.Deserialize<T>(content, response, info);
        }
    }
}
