using JiraDance.FieldManagement.Conditions;
using JiraDance.FieldManagement.Updaters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JiraDance.FieldManagement
{
    /// <summary>
    /// A class to convert from ITemplate.
    /// </summary>

    public class FieldConverter : JsonConverter
    {
        public override bool CanWrite => false;
        public override bool CanRead => true;


        /// <summary>
        /// Checks if the type can be converted.
        /// </summary>
        /// <param name="pObjectType">The object type.</param>
        /// <returns></returns>
        public override bool CanConvert(Type pObjectType)
        {
            return pObjectType == typeof(IFieldCondition) || pObjectType == typeof(IFieldUpdater);
        }

        /// <summary>
        /// Writes an object to JSON.
        /// </summary>
        /// <param name="pWriter">The writer.</param>
        /// <param name="pValue">The value.</param>
        /// <param name="pSerializer">The serializer.</param>
        public override void WriteJson(JsonWriter pWriter, object pValue, JsonSerializer pSerializer)
        {
            throw new InvalidOperationException("Use default serialization.");
        }

        /// <summary>
        /// Checks the type to create.
        /// </summary>
        /// <param name="pReader">The JSON reader.</param>
        /// <param name="pObjectType">The object type.</param>
        /// <param name="pEXistingValue"></param>
        /// <param name="pSerializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader pReader, Type pObjectType, object pEXistingValue, JsonSerializer pSerializer)
        {
            var lJsonObject = JObject.Load(pReader);
            object lResult = null;

            if (lJsonObject["Type"].Value<string>() == "IfAllChildrenEqualTo")
            {
                lResult = new IfAllChildrenEqualTo();
            }
            else if (lJsonObject["Type"].Value<string>() == "IfAtLeastOneChildEqualsTo")
            {
                lResult = new IfAtLeastOneChildEqualsTo();
            }
            else if (lJsonObject["Type"].Value<string>() == "IfNoChildrenEqualsTo")
            {
                lResult = new IfNoChildrenEqualsTo();
            }
            else if (lJsonObject["Type"].Value<string>() == "NoCondition")
            {
                lResult = new NoCondition();
            }
            else if (lJsonObject["Type"].Value<string>() == "SetValue")
            {
                lResult = new SetValue();
            }
            else if (lJsonObject["Type"].Value<string>() == "NoChild")
            {
                lResult = new NoChild();
            }
            
            pSerializer.Populate(lJsonObject.CreateReader(), lResult);
            return lResult;
        }
    }
}
