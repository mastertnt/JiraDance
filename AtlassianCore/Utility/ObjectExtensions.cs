using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlassianCore.Utility
{
    public static class ObjectExtensions
    {
        public static object GetPropertyValue(this object src, string propName)
        {
            return src.GetType().GetProperty(propName)?.GetValue(src, null);
        }

        public static void SetPropertyValue<T>(this object src, string propName, T propValue)
        {
            src.GetType().GetProperty(propName)?.SetValue(src, propValue);
        }
    }
}
