using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace PGFrammework.Res
{
    public class AddressableComponent : MonoBehaviour, IResourse
    {
        void IResourse.LoadAssets<T>(string varPath, LoadResourcesCallback Callback)
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
            var OperationHandle = Addressables.LoadAssetAsync<UnityEngine.Object>(varPath);
            yield return OperationHandle.WaitForCompletion();

            Callback?.Invoke(varPath, OperationHandle.Result, null);
        }
        private IEnumerator LoadAsset<T>(string varPath, LoadResourcesCallback Callback) where T: UnityEngine.Object
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