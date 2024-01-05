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

        public void Init(UIGroupInfo varInfo)
        {
            m_Info = varInfo;
        }


        public void OpenUI(string assetName, LoadUISuccess uISuccess, LoadUIFail uIFail)
        {
            ResourcesComponent component = GameComponent.Instance.GetComponent<ResourcesComponent>();

            UIUserData userData = new UIUserData();
            userData.uISuccess = uISuccess;
            userData.uIFail = uIFail;

            LoadAssetCallbacks loadAsset = new LoadAssetCallbacks(LoadUISuccess, LoadUIFail, userData);

            component.AsynLoadAsset(assetName, loadAsset);

        }
        public void CloseUI(UIInfo uIInfo)
        { 
        }



        private void LoadUISuccess(string assetName, object asset, object userData)
        {
            UIUserData date = (UIUserData)userData;
        }
        private void LoadUIFail(string assetName, string errorMessage, object userData)
        {
            UIUserData date = (UIUserData)userData;

            date.uIFail?.Invoke($"{assetName} load fail \n {errorMessage}");
        }



        private struct UIUserData
        {
            public LoadUISuccess uISuccess;
            public LoadUIFail uIFail;
        }
    }
}
