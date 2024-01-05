using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGFrammework.Runtime
{
    public class Version
    {
        private static IVersionHelper m_VersionHelper;

        /// <summary>
        /// 获取框架的版本号
        /// </summary>
        public static string GameFrameworkVersion { get; }
        /// <summary>
        /// 获取游戏版本号
        /// </summary>
        public static string GameVersion
        {
            get
            {
                if (m_VersionHelper == null)
                    return "1.0.0";
                return m_VersionHelper.GameVersion;
            }
        }
        /// <summary>
        /// 获取内部游戏版本号
        /// </summary>
        public static int InternalGameVersion
        {
            get
            {
                if (m_VersionHelper == null)
                    return 0;
                return m_VersionHelper.InternalGameVersion;
            }
        }

        /// <summary>
        /// 设置版本号辅助器
        /// </summary>
        /// <param name="versionHelper">要设置的版本号辅助器</param>
        public static void SetVersionHelper(IVersionHelper versionHelper)
        {
            if (versionHelper == null)
            {
                throw new Exception();
            }
            m_VersionHelper = versionHelper;
        }





        public interface IVersionHelper
        {
            /// <summary>
            /// 游戏版本号
            /// </summary>
            string GameVersion { get; }
            /// <summary>
            /// 获取内部游戏版本号
            /// </summary>
            int InternalGameVersion { get; }
        }
    }
}
