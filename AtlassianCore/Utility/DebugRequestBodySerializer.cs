using Newtonsoft.Json;
using RestEase;
using System.IO;
using System.Net.Http;

namespace AtlassianCore.Utility
{
    public class DebugRequestBodySerializer : JsonRequestBodySerializer
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
        public int RequestIndex
        {
            get;
            set;
        }

        public override HttpContent SerializeBody<T>(T body, RequestBodySerializerInfo info)
        {
            if (Directory.Exists(this.DebugPath))
            {
                // Consider caching generated XmlSerializers
                var serializer = new JsonSerializer();
                using (var stringWriter = new StringWriter())
                {
                    serializer.Serialize(stringWriter, body);
                    File.WriteAllText(this.DebugPath + @"\request_" + this.RequestIndex + ".json", stringWriter.ToString());
                    this.RequestIndex++;
                }

                
            }

            return base.SerializeBody(body, info);
        }
    }
}
