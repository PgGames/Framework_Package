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

        private List<UICanvasBase> m_AllUICanvas = new List<UICanvasBase>();
        private List<UIInfo> m_AllUIInfo = new List<UIInfo>();


        public void Init(UIGroupInfo varInfo)
        {
            m_Info = varInfo;
        }

        public void OpenUI(string assetName,LoadUIResult loadUIResult,object usedata)
        {
            OpenUI(assetName, loadUIResult, null, null, usedata);
        }
        public void OpenUI(string assetName, LoadUISuccess uISuccess, LoadUIFail uIFail,object usedata)
        {
            OpenUI(assetName, null, uISuccess, uIFail, usedata);
        }
        public void CloseUI(UIInfo uIInfo)
        { 
        }
        public void CloseUI(UICanvasBase uiBase)
        { 
        }
        public void DestroyUI(UIInfo uIInfo)
        {
        }
        public void DestroyUI(UICanvasBase uiBase)
        {
        }
        /// <summary>
        /// 清除所有UI界面
        /// </summary>
        public void ClearAllUI()
        {
        }

        private void OpenUI(string assetName, LoadUIResult result, LoadUISuccess success, LoadUIFail fail, object usedata)
        {
            ResourcesComponent component = GameComponent.Instance.GetComponent<ResourcesComponent>();

            UIUserData userData = new UIUserData();
            userData.result = result;
            userData.usedata = usedata;
            userData.success = success;
            userData.fail = fail;
            userData.usedata = usedata;

            LoadAssetCallbacks loadAsset = new LoadAssetCallbacks(LoadUISuccess, LoadUIFail, userData);

            component.AsynLoadAsset(assetName, loadAsset);
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

            UICanvasBase uICanvasBase = clone.GetComponent<UICanvasBase>();

            uICanvasBase.transform.SetParent(this.transform);
            uICanvasBase.transform.localPosition = Vector3.zero;
            uICanvasBase.transform.localEulerAngles = Vector3.zero;
            uICanvasBase.transform.localScale = Vector3.one;

            uICanvasBase.InitUI(m_Info, new UIInfo(assetName, m_Info.Depth) { UIDepth = m_AllUICanvas.Count + 1 }, date.usedata);

            m_AllUICanvas.Add(uICanvasBase);

            date.result?.Invoke(uICanvasBase, null, date.usedata);
            date.success?.Invoke(uICanvasBase, userData);


            uICanvasBase.OpenUI(date.usedata);
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
    }
}
