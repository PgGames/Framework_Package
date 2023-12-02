using System;

namespace PGFrammework.Runtime
{
    /// <summary>
    /// 资源加载失败回调
    /// </summary>
    /// <param name="assetName">资源名称</param>
    /// <param name="errorMessage">错误信息</param>
    /// <param name="userData">用户自定义数据</param>
    public delegate void LoadFailCallbacks(string assetName, string errorMessage, object userData);
    /// <summary>
    /// 加载成功回调
    /// </summary>
    /// <param name="assetName">资源名称</param>
    /// <param name="asset">资源</param>
    /// <param name="userData">用户自定义数据</param>
    public delegate void LoadSuccessCallbacks(string assetName, object asset, object userData);


    public class LoadAssetCallbacks
    {
        private readonly LoadSuccessCallbacks m_LoadSuccessCallbacks;
        private readonly LoadFailCallbacks m_LoadFailCallbacks;
        private object m_UserData;

        /// <summary>
        /// 加载回调
        /// </summary>
        /// <param name="loadSuccess">加载成功回调</param>
        public LoadAssetCallbacks(LoadSuccessCallbacks loadSuccess) : this(loadSuccess, null, null)
        {
        }
        /// <summary>
        /// 加载回调
        /// </summary>
        /// <param name="loadSuccess">加载成功回调</param>
        /// <param name="userData">用户自定义数据</param>
        public LoadAssetCallbacks(LoadSuccessCallbacks loadSuccess, object userData) : this(loadSuccess, null, userData)
        {
        }
        /// <summary>
        /// 加载回调
        /// </summary>
        /// <param name="loadSuccess">加载成功回调</param>
        /// <param name="loadFail">加载失败回调</param>
        public LoadAssetCallbacks(LoadSuccessCallbacks loadSuccess, LoadFailCallbacks loadFail) : this(loadSuccess, loadFail, null)
        {
        }
        /// <summary>
        /// 加载回调
        /// </summary>
        /// <param name="loadSuccess">加载成功回调</param>
        /// <param name="loadFail">加载失败回调</param>
        /// <param name="userData">用户自定义数据</param>
        public LoadAssetCallbacks(LoadSuccessCallbacks loadSuccess, LoadFailCallbacks loadFail, object userData)
        {
            if (loadSuccess == null)
            {
                throw new Exception("Load asset success callback is invalid");
            }
            m_LoadSuccessCallbacks = loadSuccess;
            m_LoadFailCallbacks = loadFail;
            m_UserData = userData;
        }

        public LoadSuccessCallbacks LoadSuccessCallbacks
        {
            get
            {
                return m_LoadSuccessCallbacks;
            }
        }
        public LoadFailCallbacks LoadFailCallbacks
        {
            get
            {
                return m_LoadFailCallbacks;
            }
        }
        public void InvokeSuccess(string assetName, object assets)
        {
            if (m_LoadSuccessCallbacks != null)
            {
                m_LoadSuccessCallbacks.Invoke(assetName, assets, m_UserData);
            }
        }
        public void InvokeFail(string assetName, string error)
        {
            if (m_LoadFailCallbacks != null)
            {
                m_LoadFailCallbacks.Invoke(assetName, error, m_UserData);
            }
        }
    }

}
