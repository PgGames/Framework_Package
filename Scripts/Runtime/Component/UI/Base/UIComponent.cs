using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PGFrammework.UI;

namespace PGFrammework.Runtime
{
    public class UIComponent : FrameworkComponent
    {
        [SerializeField, DisplayOnly] private List<UIGroupInfo> m_Groups = new List<UIGroupInfo>();
        /// <summary>
        /// 定期回收关闭的窗口
        /// </summary>
        [SerializeField, DisplayOnly] private bool m_RegularRecycling = true;
        /// <summary>
        /// 回收间隔
        /// </summary>
        [SerializeField, DisplayOnly] private float m_RecoveryTimes = 60;

        private Transform m_UIRoot;

        /// <summary>
        /// 所有的UI组
        /// </summary>
        private Dictionary<int, UIGroupBase> m_AllGroup = new Dictionary<int, UIGroupBase>();


        public override void Init()
        {
            GameObject tempGame = new GameObject("UIRoot");

            tempGame.transform.SetParent(this.transform);
            m_UIRoot = tempGame.transform;

            foreach (var item in m_Groups)
            {
                AddGroup(item);
            }
        }


        public void SetRoot(Transform varParent)
        {
            m_UIRoot.SetParent(varParent, false);
            m_UIRoot.transform.localPosition = Vector3.zero;
            m_UIRoot.transform.localEulerAngles = Vector3.zero;
            m_UIRoot.transform.localScale = Vector3.one;
        }


        public void AddGroup(string varGroupName, int varDepth)
        {
            AddGroup(UIGroupInfo.GetInfo(varGroupName, varDepth));
        }

        public void OpenUI(string assetsName, int varDepth, LoadUIResult loadUIResult,object usedata = null)
        {
            if (m_AllGroup.ContainsKey(varDepth))
            {
                m_AllGroup[varDepth].OpenUI(assetsName, loadUIResult, usedata);
            }
            else
            {
                FrameworkLog.Fatal($"UI Group Depth :{varDepth} non existent !");
            }
        }

        public void OpenUI(string assetsName, int varDepth ,LoadUISuccess uISuccess, LoadUIFail uIFail = null,object userData = null)
        {
            if (m_AllGroup.ContainsKey(varDepth))
            {
                m_AllGroup[varDepth].OpenUI(assetsName, uISuccess, uIFail, userData);
            }
            else
            {
                FrameworkLog.Fatal($"UI Group Depth :{varDepth} non existent !");
            }
        }


        public void CloseUI(UIInfo uIInfo)
        {
            if (m_AllGroup.ContainsKey(uIInfo.GroupDepth))
            {
                m_AllGroup[uIInfo.GroupDepth].CloseUI(uIInfo);
            }
            else
            {
                FrameworkLog.Fatal($"UI Group Depth :{uIInfo.GroupDepth} non existent !");
            }
        }





        private void AddGroup(UIGroupInfo info)
        {
            if (m_AllGroup.ContainsKey(info.Depth))
            {
                FrameworkLog.Fatal("UI Group Depth Repeat !");
            }
            else
            {
                GameObject tempClone = new GameObject(info.GroupName);

                tempClone.transform.SetParent(m_UIRoot,false);
                tempClone.transform.localPosition = Vector3.zero;
                tempClone.transform.localEulerAngles = Vector3.zero;
                tempClone.transform.localScale = Vector3.one;

                UIGroupBase groupBase =  tempClone.AddComponent<UIGroupBase>();

                info.RegularRecycling = m_RegularRecycling;
                info.RecoveryTimes = m_RecoveryTimes;

                groupBase.Init(info);
            }
        }

    }
}