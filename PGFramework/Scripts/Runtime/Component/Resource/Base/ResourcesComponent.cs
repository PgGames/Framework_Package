using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PGFrammework.Res;

namespace PGFrammework.Runtime
{
    public class ResourcesComponent : FrameworkComponent, IResourcesComponent
    {
        /// <summary>
        /// 队列限制
        /// </summary>
        [SerializeField, Range(1, 20)] private int m_QueueCount = 2;
        /// <summary>
        /// 资源加载方式
        /// </summary>
        [SerializeField] private ResourcesLoadType m_LoadMode = ResourcesLoadType.AssetBundle;


        private UnityEditorComponent m_Editor;
        private AssetBundleComponent m_AssetBundle;
        /// <summary>
        /// 待加载资源
        /// </summary>
        private Dictionary<string, List<LoadAssetCallbacks>> m_StayLoad = new Dictionary<string, List<LoadAssetCallbacks>>();
        /// <summary>
        /// 等待加载队列
        /// </summary>
        private List<AssetsKey> m_WaitLoadQueue = new List<AssetsKey>();
        /// <summary>
        /// 正在加载的队列
        /// </summary>
        private List<string> m_CurrentLoadQueue = new List<string>();

        public override void Init()
        {
            if (m_AssetBundle == null)
            {
                GameObject tempGame = new GameObject("AssetBundle");
                tempGame.transform.SetParent(this.transform);
                m_AssetBundle = tempGame.AddComponent<AssetBundleComponent>();
            }

#if !UNITY_EDITOR
            if (m_LoadMode == ResourcesLoadType.Editor)
            {
                m_LoadMode = ResourcesLoadType.AssetBundle;
            }
#endif

        }
        public void AsynLoadAsset(string assetsName, LoadAssetCallbacks loadAsset)
        {
            AsynLoadAsset(assetsName, 0, loadAsset);
        }

        public void AsynLoadAsset(string assetsName, int priority, LoadAssetCallbacks loadAsset)
        {
            AssetsKey assets = new AssetsKey
            {
                assetsName = assetsName,
                priority = priority,
                scene = false
            };
            AsynLoadResources(assets, loadAsset);
        }

        public void AsynLoadScene(string assetsName, LoadAssetCallbacks loadAsset)
        {
            AsynLoadScene(assetsName, 0, loadAsset);
        }

        public void AsynLoadScene(string assetsName, int priority, LoadAssetCallbacks loadAsset)
        {
            AssetsKey assets = new AssetsKey
            {
                assetsName = assetsName,
                priority = priority,
                scene = true
            };
            AsynLoadResources(assets, loadAsset);
        }

        /// <summary>
        /// 异步加载资源
        /// </summary>
        /// <param name="assetsKey"></param>
        /// <param name="loadAsset"></param>
        private void AsynLoadResources(AssetsKey assetsKey, LoadAssetCallbacks loadAsset)
        {
            string assetsName = assetsKey.assetsName;
            int priority = assetsKey.priority;
            bool scene = assetsKey.scene;

            if (m_StayLoad.ContainsKey(assetsName))
            {
                if (!m_StayLoad[assetsName].Contains(loadAsset))
                {
                    m_StayLoad[assetsName].Add(loadAsset);
                }
            }
            else
            {
                List<LoadAssetCallbacks> callbacks = new List<LoadAssetCallbacks>();
                callbacks.Add(loadAsset);
                m_StayLoad.Add(assetsName, callbacks);
            }
            //资源正在加载中忽略再次的加载请求
            if (m_CurrentLoadQueue.Contains(assetsName))
                return;

            if (m_CurrentLoadQueue.Count < m_QueueCount)
            {
                m_CurrentLoadQueue.Add(assetsName);

                ResourcesLoad(assetsName, scene);
                return;
            }
            bool IsContains = false;
            for (int i = 0; i < m_WaitLoadQueue.Count; i++)
            {
                AssetsKey assets = m_WaitLoadQueue[i];
                if (string.Equals(assets.assetsName, assetsName))
                {
                    IsContains = true;
                    //相同模型获取队列中的最高优先级
                    if (assets.priority > priority)
                    {
                        //无需重新进行优先级排序
                        return;
                    }
                    //重新设置优先级
                    assets.priority = priority;
                    m_WaitLoadQueue[i] = assets;
                    break;
                }
            }
            if (!IsContains)
            {
                m_WaitLoadQueue.Add(assetsKey);
            }
            //重新排序
            m_WaitLoadQueue.Sort((assetsA, assetsB) => { return assetsB.priority - assetsA.priority; });
        }
        /// <summary>
        /// 异步加载资源
        /// </summary>
        /// <param name="assetsName"></param>
        /// <param name="asset"></param>
        /// <param name="error"></param>
        private void LoadAssetAsyn(string assetsName, UnityEngine.Object asset, string error)
        {
            if (m_CurrentLoadQueue.Contains(assetsName))
            {
                m_CurrentLoadQueue.Remove(assetsName);
            }
            if (m_StayLoad.ContainsKey(assetsName))
            {
                var assetscallback = m_StayLoad[assetsName];
                m_StayLoad.Remove(assetsName);

                if (string.IsNullOrEmpty(error))
                {
                    for (int i = 0; i < assetscallback.Count; i++)
                    {
                        assetscallback[i].InvokeSuccess(assetsName, asset);
                    }
                }
                else
                {
                    for (int i = 0; i < assetscallback.Count; i++)
                    {
                        assetscallback[i].InvokeFail(assetsName, error);
                    }
                }
            }
            //存在待加载资源是继续进行加载
            if (m_WaitLoadQueue.Count != 0)
            {
                AssetsKey assets = m_WaitLoadQueue[0];

                m_WaitLoadQueue.RemoveAt(0);
                m_CurrentLoadQueue.Add(assets.assetsName);

                ResourcesLoad(assets.assetsName, assets.scene);
            }
        }
        /// <summary>
        /// 资源加载
        /// </summary>
        /// <param name="assetsName"></param>
        /// <param name="scene"></param>
        private void ResourcesLoad(string assetsName, bool scene)
        {
            IResourse resourse = null;
            switch (m_LoadMode)
            {
                case ResourcesLoadType.AssetBundle:
                    resourse = m_AssetBundle;
                    break;
                case ResourcesLoadType.Editor:
                    resourse = m_Editor;
                    break;
                default:
                    throw new System.Exception($"current LoadMode {m_LoadMode} is not exist");
            }
            if (scene)
            {
                resourse.LoadScene(assetsName, LoadAssetAsyn);
            }
            else
            {
                resourse.LoadAssets(assetsName, LoadAssetAsyn);
            }
        }


        /// <summary>
        /// AssetBundle加载
        /// </summary>
        /// <param name="assetsName"></param>
        /// <param name="scene"></param>
        private void AssetBudleLoad(string assetsName,bool scene)
        {
            if (scene)
            {
                m_AssetBundle.LoadScene(assetsName, LoadAssetAsyn);
            }
            else
            {
                m_AssetBundle.LoadAssets(assetsName, LoadAssetAsyn);
            }
        }


        /// <summary>
        /// 资源加载key
        /// </summary>
        public struct AssetsKey
        {
            /// <summary>
            /// 资源名称
            /// </summary>
            public string assetsName;
            /// <summary>
            /// 优先级
            /// </summary>
            public int priority;
            /// <summary>
            /// 是否为场景
            /// </summary>
            public bool scene;
        }
        /// <summary>
        /// 资源加载类型
        /// </summary>
        public enum ResourcesLoadType
        { 
            /// <summary>
            /// 
            /// </summary>
            AssetBundle,
            Editor,
        }
    }
}