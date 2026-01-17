using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PGFrammework.Res
{
    public class UnityEditorComponent : MonoBehaviour, IResourse
    {
        public void LoadAssets(string varPath, LoadResourcesCallback Callback)
        {
#if UNITY_EDITOR
#endif
        }

        public void LoadAssets<T>(string varPath, LoadResourcesCallback Callback) where T : UnityEngine.Object
        {
            throw new NotImplementedException();
        }

        public void LoadAssets(string assetsPath, Type assetsType, LoadResourcesCallback Callback)
        {
            throw new NotImplementedException();
        }

        public void LoadScene(string varPath, LoadResourcesCallback Callback)
        {
#if UNITY_EDITOR
#endif
        }

        public void LoadScene(string varPath, LoadSceneFinish Callback)
        {
            throw new NotImplementedException();
        }
    }
}