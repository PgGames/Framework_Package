using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace PGFrammework.Res
{
    public class AssetBundleComponent : MonoBehaviour, IResourse
    {
        /// <summary>
        /// AssetBundle资源依赖
        /// </summary>
        private AssetBundleManifest manifest;
        /// <summary>
        /// 已加载的Assetbundle
        /// </summary>
        Dictionary<string, AssetBundle> All_Assetbundle = new Dictionary<string, AssetBundle>();
        /// <summary>
        /// 正在加载的AssetBundle
        /// </summary>
        List<string> AllCurrentAssetBundle = new List<string>();



        private void GetManifest()
        {
            string assetbundlepath = $"{AssetBundleConfig.AssetBundlePath}/{AssetBundleConfig.Platform}";
            AssetBundle assetBundle = AssetBundle.LoadFromFile(assetbundlepath);
            if (assetBundle != null)
            {
                manifest = assetBundle.LoadAsset<AssetBundleManifest>(AssetBundleConfig.AssetBundleManifest);
            }
        }

        #region 异步加载资源

        void IResourse.LoadAssets<T>(string varPath, LoadResourcesCallback Callback)
        {
            StartCoroutine(LoadAsyncAssets<T>(varPath, Callback));
        }
        void IResourse.LoadAssets(string assetsPath, Type assetsType, LoadResourcesCallback Callback)
        {
            StartCoroutine(LoadAsyncAssets(assetsPath, assetsType, Callback));
        }
        IEnumerator LoadAsyncAssets<T>(string varPath, LoadResourcesCallback Callback) where T: UnityEngine.Object
        {
            yield return null;
            string filename = Path.GetFileNameWithoutExtension(varPath);
            string assetbundlename = GetAssetBundleName(varPath);
            AssetBundle assetBundle = null;
            yield return StartCoroutine(LoadAsyncAssetBundle(assetbundlename, (varassetbundle) =>
            {
                assetBundle = varassetbundle as AssetBundle;
            }));
            if (assetBundle != null)
            {
                if (assetBundle.Contains(filename))
                {
                    T assets = assetBundle.LoadAsset<T>(filename);
                    Callback.Invoke(varPath, assets, "");
                }
                else
                {
                    Callback.Invoke(varPath, null, "资源不存在");
                }
            }
            else
            {
                Callback.Invoke(varPath, null, "资源不存在");
            }
        }

        IEnumerator LoadAsyncAssets(string varPath, Type assetsType, LoadResourcesCallback Callback)
        {
            yield return null;
            string filename = Path.GetFileNameWithoutExtension(varPath);
            string assetbundlename = GetAssetBundleName(varPath);
            AssetBundle assetBundle = null;
            yield return StartCoroutine(LoadAsyncAssetBundle(assetbundlename, (varassetbundle) =>
            {
                assetBundle = varassetbundle as AssetBundle;
            }));
            if (assetBundle != null)
            {
                if (assetBundle.Contains(filename))
                {
                    var assets = assetBundle.LoadAsset(filename , assetsType);
                    Callback.Invoke(varPath, assets, "");
                }
                else
                {
                    Callback.Invoke(varPath, null, "资源不存在");
                }
            }
            else
            {
                Callback.Invoke(varPath, null, "资源不存在");
            }
        }

        #endregion

        #region 异步加载场景

        /// <summary>
        /// 加载场景
        /// </summary>
        /// <param name="varPath"></param>
        /// <param name="Callback"></param>
        public void LoadScene(string varPath, LoadSceneFinish Callback)
        {
            StartCoroutine(LoadAsyncScene(varPath, Callback));
        }
        IEnumerator LoadAsyncScene(string varPath, LoadSceneFinish Callback)
        {
            yield return null;
            string filename = Path.GetFileNameWithoutExtension(varPath);
            string assetbundlename = GetAssetBundleName(varPath);
            AssetBundle assetBundle = null;
            yield return StartCoroutine(LoadAsyncAssetBundle(assetbundlename, (varassetbundle) =>
            {
                assetBundle = varassetbundle as AssetBundle;
            }));
            if (assetBundle != null)
            {
                Callback.Invoke(varPath, "");
            }
            else
            {
                Callback.Invoke(varPath, "资源不存在");
            }
        }


        #endregion

        #region 加载逻辑

        /// <summary>
        /// 异步加载AssetBundle
        /// </summary>
        /// <param name="varAssetBundleName"></param>
        /// <param name="assetBundleFinish"></param>
        /// <returns></returns>
        IEnumerator LoadAsyncAssetBundle(string varAssetBundleName, LoadResourcesFinish<AssetBundle> assetBundleFinish)
        {
            //确保Key值统一标准
            varAssetBundleName = varAssetBundleName.Replace("\\", "/");

            if (All_Assetbundle.ContainsKey(varAssetBundleName))
            {
                //获取AssetBundle
                AssetBundle assetBundle = All_Assetbundle[varAssetBundleName];
                //完成回调
                assetBundleFinish?.Invoke(assetBundle);
            }
            else if (AllCurrentAssetBundle.Contains(varAssetBundleName))
            {
                while (AllCurrentAssetBundle.Contains(varAssetBundleName))
                {
                    yield return null;
                }
                //获取AssetBundle
                AssetBundle assetBundle = All_Assetbundle[varAssetBundleName];
                //完成回调
                assetBundleFinish?.Invoke(assetBundle);
            }
            else
            {
                //添加加载标记
                string assetbundlepath = $"{AssetBundleConfig.AssetBundlePath}/{varAssetBundleName}";

                AllCurrentAssetBundle.Add(varAssetBundleName);
                AssetBundle tempAssetBundle = null;
                AssetBundleCreateRequest tempAssetbundleQuest = AssetBundle.LoadFromFileAsync(assetbundlepath);
                while (!tempAssetbundleQuest.isDone)
                {
                    yield return null;
                }
                tempAssetBundle = tempAssetbundleQuest.assetBundle;
                All_Assetbundle.Add(varAssetBundleName, tempAssetBundle);
                //加载依赖
                yield return StartCoroutine(LoadAsyncAssetBundleManifest(tempAssetBundle, varAssetBundleName));
                //完成回调
                assetBundleFinish?.Invoke(tempAssetBundle);
                //移除加载标记
                AllCurrentAssetBundle.Remove(varAssetBundleName);
            }
        }
        /// <summary>
        /// 加载AssetBundle的依赖
        /// </summary>
        /// <param name="assetBundle"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        IEnumerator LoadAsyncAssetBundleManifest(AssetBundle assetBundle, string filename)
        {
            if (manifest == null)
            {
                GetManifest();
            }
            if (manifest != null)
            {
                string[] Dependencies = manifest.GetDirectDependencies(filename);
                if (Dependencies != null)
                {
                    for (int i = 0; i < Dependencies.Length; i++)
                    {
                        yield return StartCoroutine(LoadAsyncAssetBundle(Dependencies[i], null));
                    }
                }
            }
        }
        /// <summary>
        /// 获取assetbundle名称
        /// </summary>
        /// <param name="varAssetbunlePath"></param>
        /// <returns></returns>
        private string GetAssetBundleName(string varPath)
        {
            string tempPath = Path.GetDirectoryName(varPath).ToLower();
            tempPath = tempPath.Replace("\\", "/");
            if (tempPath.Contains(AssetBundleConfig.AssetBundlePrefix))
            {
                int index = tempPath.IndexOf(AssetBundleConfig.AssetBundlePrefix);
                if (index >= 0)
                {
                    string assbundlename = tempPath.Remove(0, index + AssetBundleConfig.AssetBundlePrefix.Length);

                    return $"{assbundlename}.{AssetBundleConfig.AssetBundleVariant}";
                }
                else
                {
                    return $"{varPath}.{AssetBundleConfig.AssetBundleVariant}";
                }
            }
            else
            {
                return $"{varPath}.{AssetBundleConfig.AssetBundleVariant}";
            }
        }

        #endregion
    }
}
