using PGFrammework.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourseDemo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameEntity.Resources.AsynLoadAsset<GameObject>("HotResources/Prefabs/Cube 1.prefab", new LoadAssetCallbacks((assetpath, asset, ass)=> {
            if (asset != null)
            {
                GameObject clone = GameObject.Instantiate(asset as GameObject);
            }
        }));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
