using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        public void LoadScene(string varPath, LoadResourcesCallback Callback)
        {
#if UNITY_EDITOR
#endif
        }
    }
}