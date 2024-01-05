using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGFrammework
{
    public static partial class Utility
    {
        public static class Text
        {
            private const int StringBuilderCapacity = 1024;
            [ThreadStatic]
            private static StringBuilder s_CachedStringBuilder = null;

            /// <summary>
            /// 获取格式化字符串。
            /// </summary>
            /// <param name="format">字符串格式。</param>
            /// <param name="arg0">字符串参数 0。</param>
            /// <returns>格式化后的字符串。</returns>
            public static string Format(string format,params object[] arg0)
            {
                if (format == null)
                {
                    throw new Exception("Format is invalid.");
                }
                CheckCachedStringBuilder();
                s_CachedStringBuilder.Length = 0;
                s_CachedStringBuilder.AppendFormat(format, arg0);
                return s_CachedStringBuilder.ToString();
            }


            private static void CheckCachedStringBuilder()
            {
                if (s_CachedStringBuilder == null)
                {
                    s_CachedStringBuilder = new StringBuilder(StringBuilderCapacity);
                }
            }
        }
    }
}
