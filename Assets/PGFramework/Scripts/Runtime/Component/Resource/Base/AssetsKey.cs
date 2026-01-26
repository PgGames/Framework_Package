using System;
using System.Collections;
using UnityEngine;

namespace PGFrammework.Res
{
    public class AssetsKey
    {
        /// <summary>
        /// 资源名称
        /// </summary>
        public string assetsName;
        /// <summary>
        /// 资源类型
        /// </summary>
        public Type assetsType;
        /// <summary>
        /// 优先级
        /// </summary>
        public int priority;
        /// <summary>
        /// 是否为场景
        /// </summary>
        public bool scene;

        public virtual void LoadAsset(IResourse resourse, LoadResourcesCallback Callback)
        {
            //LoadResourcesCallback callback = Callback as LoadResourcesCallback;
            resourse.LoadAssets(assetsName, assetsType, Callback);
        }

        public virtual void LoadScene(IResourse resourse, LoadSceneFinish Callback)
        {
            resourse.LoadScene(assetsName, Callback);
        }
    }
    public class AssetKey<TObject> : AssetsKey where TObject : UnityEngine.Object
    {
        public override void LoadAsset(IResourse resourse, LoadResourcesCallback Callback)
        {
            //LoadResourcesCallback<TObject> callback = Callback as LoadResourcesCallback<TObject>;
            resourse.LoadAssets<TObject>(assetsName, Callback);
        }
    }
}