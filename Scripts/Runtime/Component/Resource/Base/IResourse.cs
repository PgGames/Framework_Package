
using System;

namespace PGFrammework.Res
{
    public interface IResourse
    {
        /// <summary>
        /// 加载资源
        /// </summary>
        /// <param name="varPath"></param>
        /// <param name="Callback"></param>
        void LoadAssets<T>(string varPath, LoadResourcesCallback<T> Callback) where T : UnityEngine.Object;
        /// <summary>
        /// 加载资源
        /// </summary>
        /// <param name="assetsPath">资源路径</param>
        /// <param name="assetsType">资源类型</param>
        /// <param name="Callback">加载资源回调</param>
        void LoadAssets(string assetsPath, Type assetsType, LoadResourcesCallback Callback);
        /// <summary>
        /// 加载场景
        /// </summary>
        /// <param name="varPath"></param>
        /// <param name="Callback"></param>
        void LoadScene(string varPath, LoadSceneFinish Callback);
    }
}
