using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGFrammework.Runtime
{
    public class FrameworkJson
    {
        private static IJsonHelper m_JsonHelper = null;

        /// <summary>
        /// 将对象序列化为 JSON 字符串。
        /// </summary>
        /// <param name="obj">要序列化的对象</param>
        /// <returns>序列化后的 JSON 字符串</returns>
        public static string ToJson(object obj)
        {
            if (m_JsonHelper == null)
                throw new Exception("json helper is null");
            return m_JsonHelper.ToJson(obj);
        }
        /// <summary>
        /// 将 JSON 字符串反序列化为对象。
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="jsondata">要反序列化的 JSON 字符串。</param>
        /// <returns>反序列化后的对象。</returns>
        public static T ToObject<T>(string jsondata)
        {
            if (m_JsonHelper == null)
                throw new Exception("json helper is null");
            return m_JsonHelper.ToObject<T>(jsondata);
        }
        /// <summary>
        /// 将 JSON 字符串反序列化为对象。
        /// </summary>
        /// <param name="objectType">对象类型。</param>
        /// <param name="jsondate">要反序列化的 JSON 字符串。</param>
        /// <returns>反序列化后的对象。</returns>
        public static object ToObject(Type objectType, string jsondate)
        {
            if (m_JsonHelper == null)
                throw new Exception("json helper is null");
            return m_JsonHelper.ToObject(objectType, jsondate);
        }

        /// <summary>
        /// 设置Json辅助器
        /// </summary>
        /// <param name="varJson"></param>
        public static void SetJsonHelper(IJsonHelper varJson)
        {
            m_JsonHelper = varJson;
        }

        public interface IJsonHelper
        {
            string ToJson(object obj);
            T ToObject<T>(string jsondata);
            object ToObject(Type objectType, string jsondate);
        }

    }
}
