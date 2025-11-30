using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace PGFrammework.Res
{
    public class AddressableComponent : MonoBehaviour, IResourse
    {
        void IResourse.LoadAssets<T>(string varPath, LoadResourcesCallback<T> Callback)
        {
            StartCoroutine(LoadAsset<T>(varPath, Callback));
        }
        void IResourse.LoadAssets(string assetsPath, System.Type assetsType, LoadResourcesCallback Callback)
        {
            StartCoroutine(LoadAsset(assetsPath, assetsType, Callback));
        }
        void IResourse.LoadScene(string varPath, LoadSceneFinish Callback)
        {
            StartCoroutine(LoadSceneAync(varPath, Callback));
        }
        public void UnLoad(string varPath)
        { 
        }


        private IEnumerator LoadAsset(string varPath, System.Type assetsType, LoadResourcesCallback Callback)
        {
            var OperationHandle = Addressables.LoadResourceLocationsAsync(varPath, assetsType);
            yield return OperationHandle.WaitForCompletion();
            IList<IResourceLocation> data = OperationHandle.Result;
            foreach (var item in data)
            {
                if (item != null)
                {
                    Callback?.Invoke(varPath, item.Data as UnityEngine.Object, null);
                }
            }

        }
        private IEnumerator LoadAsset<T>(string varPath, LoadResourcesCallback<T> Callback) where T: UnityEngine.Object
        {
            var OperationHandle = Addressables.LoadAssetAsync<T>(varPath);
            yield return OperationHandle.WaitForCompletion();

            Callback?.Invoke(varPath, OperationHandle.Result, null);

        }


        private IEnumerator LoadSceneAync(string varScene, LoadSceneFinish Callback)
        {
            var OperationHandle = Addressables.LoadSceneAsync(varScene);
            yield return OperationHandle.WaitForCompletion();

            SceneInstance result = OperationHandle.Result;
            AsyncOperation operation = result.ActivateAsync();


            Callback?.Invoke(varScene, "");
        }

    }
}