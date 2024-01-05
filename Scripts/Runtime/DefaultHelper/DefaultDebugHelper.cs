using System;
using System.Collections.Generic;
using UnityEngine;

namespace PGFrammework.Runtime
{
    /// <summary>
    /// 默认游戏框架日志辅助器。
    /// </summary>
    public class DefaultDebugHelper : PGFrammework.Runtime.FrameworkLog.ILogHelper
    {
        /// <summary>
        /// 记录日志。
        /// </summary>
        /// <param name="level">日志等级。</param>
        /// <param name="message">日志内容。</param>
        public void LogInfo(FrameworkLogLevel logLevel, object message)
        {
            switch (logLevel)
            {
                case FrameworkLogLevel.Debug:
                    Debug.Log(message);
                    break;
                case FrameworkLogLevel.Warning:
                    Debug.LogWarning(message);
                    break;
                case FrameworkLogLevel.Error:
                    Debug.LogError(message);
                    break;
                case FrameworkLogLevel.Fatal:
                default:
                    throw new Exception(message.ToString());
            }
        }
    }
}
