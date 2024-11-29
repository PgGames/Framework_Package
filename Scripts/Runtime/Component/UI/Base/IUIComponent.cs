using PGFrammework.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PGFrammework.Runtime
{
    public interface IUIComponent
    {
        /// <summary>
        /// 设置UI跟节点
        /// </summary>
        /// <param name="varParent"></param>
        void SetRoot(Transform varParent);

        /// <summary>
        /// 添加UI组
        /// </summary>
        /// <param name="varGroupName"></param>
        /// <param name="varDepth"></param>
        void AddGroup(string varGroupName, int varDepth);
        /// <summary>
        /// 打开UI
        /// </summary>
        /// <param name="assetsName"></param>
        /// <param name="varDepth"></param>
        void OpenUI(string assetsName, int varDepth, LoadUIResult loadUIResult, object usedata = null, bool single = true);
        /// <summary>
        /// 打开UI
        /// </summary>
        /// <param name="assetsName"></param>
        /// <param name="varDepth"></param>
        /// <param name="uISuccess"></param>
        /// <param name="uIFail"></param>
        /// <param name="userData"></param>
        /// <param name="single"></param>
        void OpenUI(string assetsName, int varDepth, LoadUISuccess uISuccess, LoadUIFail uIFail = null, object userData = null, bool single = true);
        /// <summary>
        /// 关闭UI
        /// </summary>
        /// <param name="uIInfo"></param>
        void CloseUI(UIInfo uIInfo);
        /// <summary>
        /// 清楚UI
        /// </summary>
        /// <param name="uIInfo"></param>
        void ClearUI(UIInfo uIInfo);
        /// <summary>
        /// 清楚某组UI
        /// </summary>
        /// <param name="group"></param>
        void ClearAllByGroup(int group);
    }
}
