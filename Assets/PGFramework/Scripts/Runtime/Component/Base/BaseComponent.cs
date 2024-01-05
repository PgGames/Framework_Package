using System;
using System.Collections;
using UnityEngine;

namespace PGFrammework.Runtime
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Game PGFrammework/Base")]
    public class BaseComponent : FrameworkComponent
    {
        [SerializeField]
        private string m_VersionHelperTypeName = "PGFrammework.Runtime.DefaultVersionHelper";

        [SerializeField]
        private string m_DebugHelperTypeName = "PGFrammework.Runtime.DefaultDebugHelper";

        [SerializeField]
        private string m_JsonHelperTypeName = "PGFrammework.Runtime.DefaultJsonHelper";

        [SerializeField]
        private int m_FrameRate = 30;

        [SerializeField]
        private float m_GameSpeed = 1f;

        [SerializeField]
        private bool m_RunInBackground = false;
        [SerializeField]
        private bool m_NeverSleep = false;


        /// <summary>
        /// 获取或设置是否允许后台运行。
        /// </summary>
        public bool RunInBackground
        {
            get
            {
                return m_RunInBackground;
            }
            set
            {
                Application.runInBackground = m_RunInBackground = value;
            }
        }

        /// <summary>
        /// 获取或设置是否禁止休眠。
        /// </summary>
        public bool NeverSleep
        {
            get
            {
                return m_NeverSleep;
            }
            set
            {
                m_NeverSleep = value;
                Screen.sleepTimeout = value ? SleepTimeout.NeverSleep : SleepTimeout.SystemSetting;
            }
        }

        /// <summary>
        /// 获取或设置游戏目标帧率。
        /// </summary>
        public int FrameRate
        {
            get
            {
                return m_FrameRate;
            }
            set
            {
                Application.targetFrameRate = m_FrameRate = value;
            }
        }

        /// <summary>
        /// 获取或设置游戏速度
        /// </summary>
        public float GameSpeed
        {
            get
            {
                return m_GameSpeed;
            }
            set
            {
                Time.timeScale = m_GameSpeed = value >= 0 ? value : 0;
            }
        }

        public override void Init()
        {
            InitVersionHelper();
            InitLogHelper();
            InitJsonHelper();

            Application.targetFrameRate = FrameRate;
            Application.runInBackground = RunInBackground;
            Screen.sleepTimeout = NeverSleep ? SleepTimeout.NeverSleep : SleepTimeout.SystemSetting;
            Time.timeScale = GameSpeed;
        }



        private void InitVersionHelper()
        {
            if (string.IsNullOrEmpty(m_VersionHelperTypeName))
            {
                return;
            }

            Type tempHelperType = Utility.Assembly.GetType(m_VersionHelperTypeName);
            if (tempHelperType == null)
            {
                throw new Exception(Utility.Text.Format("Can not find version helper type '{0}'.", m_VersionHelperTypeName));
            }
            PGFrammework.Runtime.Version.IVersionHelper tempHelper = (PGFrammework.Runtime.Version.IVersionHelper)Activator.CreateInstance(tempHelperType);
            if (tempHelper == null)
            {
                throw new Exception(Utility.Text.Format("Can not find version helper instance '{0}'.", m_VersionHelperTypeName));
            }
            PGFrammework.Runtime.Version.SetVersionHelper(tempHelper);
        }
        private void InitLogHelper()
        {
            if (string.IsNullOrEmpty(m_DebugHelperTypeName))
            {
                return;
            }

            Type tempHelperType = Utility.Assembly.GetType(m_DebugHelperTypeName);
            if (tempHelperType == null)
            {
                throw new Exception(Utility.Text.Format("Can not find debug helper type '{0}'.", m_DebugHelperTypeName));
            }
            PGFrammework.Runtime.FrameworkLog.ILogHelper tempHelper = (PGFrammework.Runtime.FrameworkLog.ILogHelper)Activator.CreateInstance(tempHelperType);
            if (tempHelper == null)
            {
                throw new Exception(Utility.Text.Format("Can not find debug helper instance '{0}'.", m_DebugHelperTypeName));
            }
            PGFrammework.Runtime.FrameworkLog.SetLogHelper(tempHelper);
        }
        private void InitJsonHelper()
        {
            if (string.IsNullOrEmpty(m_JsonHelperTypeName))
            {
                return;
            }
            Type tempHelperType = Utility.Assembly.GetType(m_JsonHelperTypeName);
            if (tempHelperType == null)
            {
                throw new Exception(Utility.Text.Format("Can not find json helper type '{0}'.", m_JsonHelperTypeName));
            }
            PGFrammework.Runtime.FrameworkJson.IJsonHelper tempHelper = (PGFrammework.Runtime.FrameworkJson.IJsonHelper)Activator.CreateInstance(tempHelperType);
            if (tempHelper == null)
            {
                throw new Exception(Utility.Text.Format("Can not find json helper instance '{0}'.", m_JsonHelperTypeName));
            }
            PGFrammework.Runtime.FrameworkJson.SetJsonHelper(tempHelper);
        }
    }
}