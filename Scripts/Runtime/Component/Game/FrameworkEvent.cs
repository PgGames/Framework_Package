using System.Collections;
using UnityEngine;

namespace PGFrammework
{
    public delegate void FrameworkInitFinish();

    public enum FrameworkEvent
    {
        /// <summary>
        /// 框架初始化完成
        /// </summary>
        FrameworkFinish,
    }
}