using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGFrammework.Runtime
{
    public class FrameworkLog
    {
        private static ILogHelper m_LogHelper = null;


        /// <summary>
        /// 输出调试级别的日志
        /// </summary>
        /// <param name="message"></param>
        public static void Log(string message)
        {
            if (m_LogHelper == null)
                throw new Exception("debug helper is null");
            m_LogHelper.LogInfo(FrameworkLogLevel.Debug, message);
        }
        /// <summary>
        /// 输出警告级别的日志
        /// </summary>
        /// <param name="message"></param>
        public static void Warning(string message)
        {
            if (m_LogHelper == null)
                throw new Exception("debug helper is null");
            m_LogHelper.LogInfo(FrameworkLogLevel.Warning, message);
        }
        /// <summary>
        /// 输出错误级别的日志
        /// </summary>
        /// <param name="message"></param>
        public static void Error(string message)
        {
            if (m_LogHelper == null)
                throw new Exception("debug helper is null");
            m_LogHelper.LogInfo(FrameworkLogLevel.Error, message);
        }
        /// <summary>
        /// 输出严重错误级别的日志
        /// </summary>
        /// <param name="message"></param>
        public static void Fatal(string message)
        {
            if (m_LogHelper == null)
                throw new Exception("debug helper is null");
            m_LogHelper.LogInfo(FrameworkLogLevel.Fatal, message);
        }

        /// <summary>
        /// 设置日志辅助器
        /// </summary>
        /// <param name="varHelper"></param>
        public static void SetLogHelper(ILogHelper varHelper)
        {
            m_LogHelper = varHelper;
        }

        public interface ILogHelper
        {
            void LogInfo(FrameworkLogLevel logLevel, object message);
        }
    }
}
