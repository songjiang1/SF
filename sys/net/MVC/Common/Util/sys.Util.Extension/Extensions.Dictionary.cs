using System;
using System.Collections.Generic;

namespace sys.Util.Extension
{
    /// <summary>
    /// 验证扩展
    /// </summary>
    public static partial class Extensions
    {
        public static object GetValue(this Dictionary<string, object> dict, string key)
        {
            if (dict == null)
                return (object)null;
            if (dict.ContainsKey(key))
                return dict[key];
            return (object)null;
        }

        public static object GetValue(
          this Dictionary<string, object> dict,
          string key,
          string defaultKey)
        {
            if (dict == null)
                return (object)null;
            if (!string.IsNullOrEmpty(key) && dict.ContainsKey(key))
                return dict[key];
            if (!string.IsNullOrEmpty(defaultKey) && dict.ContainsKey(defaultKey))
                return dict[defaultKey];
            return (object)null;
        }

        public static string GetStringValue(this Dictionary<string, object> dict, string key)
        {
            if (dict == null)
                return (string)null;
            if (!dict.ContainsKey(key))
                return (string)null;
            if (dict[key].IsEmpty())
                return (string)null;
            return dict[key].ToString();
        }

    }
}
