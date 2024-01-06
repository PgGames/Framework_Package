using PGFrammework.Runtime;
using System.Collections;
using UnityEngine;

namespace PGFrammework.UI
{
    public abstract class UICanvasBase : MonoBehaviour
    {
        private UIGroupInfo m_GroupInfo;
        private UIInfo m_UIInfo;
        private Coroutine m_CloseTimer;

        public UIInfo GetUI { get => m_UIInfo; }
        public Canvas m_Canvas;

        public int GetDepth { get { return m_UIInfo.GroupDepth * 100 + m_UIInfo.UIDepth; } }

        public void InitUI(UIGroupInfo group, UIInfo info,object userdata)
        {
            m_GroupInfo = group;
            m_UIInfo = info;


            m_Canvas.sortingOrder = GetDepth;
        }
        public void OpenUI(object userdata) 
        {
            //关闭回收计时器
            if (m_CloseTimer != null)
            {
                UIComponent component = GameComponent.Instance.GetComponent<UIComponent>();
                component.StopCoroutine(m_CloseTimer);
            }
            Open(userdata);
        }
        public void CloseUI(object userdata)
        {
            //开启回收计时器
            if (m_GroupInfo.RegularRecycling)
            {
                UIComponent component = GameComponent.Instance.GetComponent<UIComponent>();
                m_CloseTimer = component.StartCoroutine(CloseTimer());
            }
            Close(userdata);
        }

        public void RecoveryUI()
        { 
        }

        private IEnumerator CloseTimer()
        {
            yield return new WaitForSeconds(m_GroupInfo.RecoveryTimes);
            RecoverySelf();
        }




        /// <summary>
        /// 界面初始化
        /// </summary>
        /// <param name="userdata"></param>
        protected abstract void Init(object userdata);
        /// <summary>
        /// 界面被开启
        /// </summary>
        /// <param name="userdata"></param>
        protected abstract void Open(object userdata);
        /// <summary>
        /// 界面被关闭
        /// </summary>
        /// <param name="userdata"></param>
        protected abstract void Close(object userdata);
        /// <summary>
        /// 界面被回收
        /// </summary>
        protected virtual void Recovery()
        { 
        }

        /// <summary>
        /// 回收界面
        /// </summary>
        protected void RecoverySelf()
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