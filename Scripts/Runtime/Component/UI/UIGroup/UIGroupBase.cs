using PGFrammework.Runtime;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PGFrammework.UI
{
    public class UIGroupBase : MonoBehaviour
    {
        private UIGroupInfo m_Info;

        public UIGroupInfo GetInfo { get => m_Info; }

        /// <summary>
        /// 所有被打开的窗口
        /// </summary>
        private List<UICanvasBase> m_AllUICanvas = new List<UICanvasBase>();
        /// <summary>
        /// 被关闭的界面
        /// </summary>
        private List<UICanvasBase> m_LastUICanvas;

        private List<UIInfo> m_AllUIInfo = new List<UIInfo>();


        public void Init(UIGroupInfo varInfo)
        {
            m_Info = varInfo;
            m_AllUICanvas = new List<UICanvasBase>();
            m_LastUICanvas = new List<UICanvasBase>();
        }

        public void OpenUI(string assetName, LoadUIResult result, object usedata = null,bool single = true)
        {
            OpenUI(assetName, result, null, null, usedata, single);
        }
        public void OpenUI(string assetName, LoadUISuccess success, LoadUIFail fail, object usedata = null, bool single = true)
        {
            OpenUI(assetName, null, success, fail, usedata, single);
        }
        public void CloseUI(UIInfo uIInfo)
        {

            UICanvasBase uibase = GetUICanvas(uIInfo);
            uibase.CloseUI();

            UpdateUIDepth(uibase, UpdateDepthType.Close);

        }
        public void RecoveryUI(UIInfo uIInfo)
        {
            UICanvasBase uibase = GetUICanvas(uIInfo);
            uibase.RecoveryUI();

            UpdateUIDepth(uibase, UpdateDepthType.RecoveryUI);


            GameObject.Destroy(uibase.gameObject);
        }
        /// <summary>
        /// 回收所有UI界面
        /// </summary>
        public void RecoveryAllUI()
        {
            foreach (var item in m_AllUICanvas)
            {
                item.RecoveryUI();
            }
            foreach (var item in m_LastUICanvas)
            {
                item.RecoveryUI();
            }

            //清除已打开的窗口
            for (int i = 0; i < m_AllUICanvas.Count; i++)
            {
                GameObject.Destroy(m_AllUICanvas[i].gameObject);
            }
            m_AllUICanvas.Clear();
            //清除已废弃的窗口
            for (int i = 0; i < m_LastUICanvas.Count; i++)
            {
                GameObject.Destroy(m_LastUICanvas[i].gameObject);
            }
            m_LastUICanvas.Clear();

        }


        /// <summary>
        /// 刷新界面深度
        /// </summary>
        /// <param name="uibase"></param>
        /// <param name="depthType"></param>
        private void UpdateUIDepth(UICanvasBase uibase, UpdateDepthType depthType)
        {
            switch (depthType)
            {
                case UpdateDepthType.Open:
                    if (m_AllUICanvas.Contains(uibase))
                        m_AllUICanvas.Remove(uibase);
                    if (m_LastUICanvas.Contains(uibase))
                        m_LastUICanvas.Remove(uibase);
                    m_AllUICanvas.Add(uibase);
                    break;
                case UpdateDepthType.Close:
                    if (m_AllUICanvas.Contains(uibase))
                        m_AllUICanvas.Remove(uibase);
                    if (!m_LastUICanvas.Contains(uibase))
                        m_LastUICanvas.Add(uibase);
                    break;
                case UpdateDepthType.RecoveryUI:
                    if (m_AllUICanvas.Contains(uibase))
                        m_AllUICanvas.Remove(uibase);
                    if (m_LastUICanvas.Contains(uibase))
                        m_LastUICanvas.Remove(uibase);
                    break;
                default:
                    break;
            }

            for (int i = 0; i < m_AllUICanvas.Count; i++)
            {
                m_AllUICanvas[i].UIDepth = i + 1;
            }
        }

        /// <summary>
        /// 获取废弃的UI窗口
        /// </summary>
        /// <param name="assetName"></param>
        /// <returns></returns>
        private UICanvasBase GetLastUICanvas(string assetName)
        {
            if (m_LastUICanvas.Count == 0)
                return null;
            for (int i = 0; i < m_LastUICanvas.Count; i++)
            {
                if (m_LastUICanvas[i].AssetsName == assetName)
                    return m_LastUICanvas[i];
            }
            return null;
        }
        /// <summary>
        /// 获取UI窗口
        /// </summary>
        /// <param name="assetName"></param>
        /// <returns></returns>
        private UICanvasBase GetUICanvas(string assetName)
        {
            UICanvasBase uibase = GetLastUICanvas(assetName);
            if (uibase != null)
                return uibase;
            if (m_AllUICanvas.Count == 0)
                return null;
            for (int i = 0; i < m_AllUICanvas.Count; i++)
            {
                if (m_AllUICanvas[i].AssetsName == assetName)
                    return m_AllUICanvas[i];
            }
            return null;
        }
        /// <summary>
        /// 获取UI窗口
        /// </summary>
        /// <param name="uIInfo"></param>
        /// <returns></returns>
        private UICanvasBase GetUICanvas(UIInfo uIInfo,bool all = false)
        {
            foreach (var item in m_AllUICanvas)
            {
                if (item.GetUI == uIInfo)
                    return item;
            }
            foreach (var item in m_LastUICanvas)
            {
                if (item.GetUI == uIInfo)
                    return item;
            }
            return null;
        }
        /// <summary>
        /// 开启窗口界面
        /// </summary>
        /// <param name="assetName">界面资源</param>
        /// <param name="result">结果回调</param>
        /// <param name="success">成功回调</param>
        /// <param name="fail">失败回调</param>
        /// <param name="usedata">自定义数据</param>
        /// <param name="single">是否为单开窗口</param>
        private void OpenUI(string assetName, LoadUIResult result, LoadUISuccess success, LoadUIFail fail, object usedata,bool single)
        {
            if (single)
            {
                UICanvasBase uibase = GetUICanvas(assetName);
                if (uibase != null)
                {
                    UpdateUIDepth(uibase, UpdateDepthType.Open);

                    uibase.OpenUI(usedata);

                    result?.Invoke(uibase, null, usedata);
                    success?.Invoke(uibase, usedata);
                }
                else
                {
                    LoadUI(assetName, result, success, fail, usedata);
                }
            }
            else
            {
                UICanvasBase uibase = GetLastUICanvas(assetName);
                if (uibase != null)
                {
                    UpdateUIDepth(uibase, UpdateDepthType.Open);

                    uibase.OpenUI(usedata);
                    result?.Invoke(uibase, null, usedata);
                    success?.Invoke(uibase, usedata);
                }
                else
                {
                    LoadUI(assetName, result, success, fail, usedata);
                }
            }
        }
        /// <summary>
        /// 加载窗口
        /// </summary>
        /// <param name="assetName"></param>
        /// <param name="result"></param>
        /// <param name="success"></param>
        /// <param name="fail"></param>
        /// <param name="usedata"></param>
        private void LoadUI(string assetName, LoadUIResult result, LoadUISuccess success, LoadUIFail fail, object usedata)
        {
            ResourcesComponent component = GameComponent.Instance.GetComponent<ResourcesComponent>();

            UIUserData userData = new UIUserData();
            userData.result = result;
            userData.usedata = usedata;
            userData.success = success;
            userData.fail = fail;
            userData.usedata = usedata;

            LoadAssetCallbacks loadAsset = new LoadAssetCallbacks(LoadUISuccess, LoadUIFail, userData);

            component.AsynLoadAsset<GameObject>(assetName, loadAsset);
        }
        /// <summary>
        /// 加载资源成功
        /// </summary>
        /// <param name="assetName"></param>
        /// <param name="asset"></param>
        /// <param name="userData"></param>
        private void LoadUISuccess(string assetName, object asset, object userData)
        {
            UIUserData date = (UIUserData)userData;
            GameObject game = asset as GameObject;

            if (game == null)
            {
                string error = $"{assetName} load fail \n asset is not exist";
                date.result?.Invoke(null, error, userData);
                date.fail?.Invoke(error, userData);
                return;
            }

            if (game.GetComponent<UICanvasBase>() == null)
            {
                string error = $"{assetName} load fail \n asset is not exist {typeof(UICanvasBase).Name} component";
                date.result?.Invoke(null, error, userData);
                date.fail?.Invoke(error, userData);
                return;
            }

            GameObject clone = GameObject.Instantiate<GameObject>(game);

            UICanvasBase uibase = clone.GetComponent<UICanvasBase>();

            uibase.transform.SetParent(this.transform);
            uibase.transform.localPosition = Vector3.zero;
            uibase.transform.localEulerAngles = Vector3.zero;
            uibase.transform.localScale = Vector3.one;

            uibase.InitUI(m_Info, new UIInfo(assetName, m_Info.Depth) { UIDepth = m_AllUICanvas.Count + 1 }, date.usedata);

            UpdateUIDepth(uibase, UpdateDepthType.Open);

            date.result?.Invoke(uibase, null, date.usedata);
            date.success?.Invoke(uibase, userData);


            uibase.OpenUI(date.usedata);
        }
        /// <summary>
        /// 加载UI资源失败
        /// </summary>
        /// <param name="assetName"></param>
        /// <param name="errorMessage"></param>
        /// <param name="userData"></param>
        private void LoadUIFail(string assetName, string errorMessage, object userData)
        {
            string error = $"{assetName} load fail \n {errorMessage}";
            UIUserData date = (UIUserData)userData;

            date.result?.Invoke(null, error, userData);
            date.fail?.Invoke(error, userData);
        }



        private struct UIUserData
        {
            public LoadUIResult result;
            public LoadUISuccess success;
            public LoadUIFail fail;
            public object usedata;
        }


        private enum UpdateDepthType
        {
            Open,
            Close,
            RecoveryUI,
        }
    }
}
