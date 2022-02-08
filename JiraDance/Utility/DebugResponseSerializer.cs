using RestEase;

namespace JiraDance
{
    /// <summary>
    /// This class is used to debug the response from rest api.
    /// </summary>
    public class DebugResponseDeserializer : JsonResponseDeserializer
    {
        /// <inheritdoc/>
        public override T Deserialize<T>(string? content, HttpResponseMessage response, ResponseDeserializerInfo info)
        {
            File.WriteAllText(@"d:\serialize.json", content);
            return base.Deserialize<T>(content, response, info);
        }
    }
}
