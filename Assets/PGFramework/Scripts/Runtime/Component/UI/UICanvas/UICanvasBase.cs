using System.Collections;
using UnityEngine;

namespace PGFrammework.UI
{
    public class UICanvasBase : MonoBehaviour
    {
        private UIGroupInfo groupInfo;
        private UIInfo uiInfo;

        public int GetDepth { get { return uiInfo.GroupDepth * 100 + uiInfo.UIDepth; } }

        public void InitUI(UIGroupInfo group, UIInfo info,object userdata)
        {
            groupInfo = group;
            uiInfo = info;
        }
        public void OpenUI(object userdata) 
        {
            Open(userdata);
        }
        public void CloseUI(object userdata)
        {
            Close(userdata);
        }
        /// <summary>
        /// 界面初始化
        /// </summary>
        /// <param name="userdata"></param>
        protected virtual void Init(object userdata)
        {
        }
        /// <summary>
        /// 界面被开启
        /// </summary>
        /// <param name="userdata"></param>
        protected virtual void Open(object userdata)
        {
        }
        /// <summary>
        /// 界面被关闭
        /// </summary>
        /// <param name="userdata"></param>
        protected virtual void Close(object userdata)
        { 
        }




        /// <summary>
        /// 关闭自身
        /// </summary>
        protected void ShutDown()
        { 
        }
    }
}