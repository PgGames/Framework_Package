
using UnityEngine;

namespace PGFrammework.Res
{
    /// <summary>
    /// 加载AssetBundle回调
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="varPath"></param>
    /// <param name="varObject"></param>
    public delegate void LoadResourcesCallback<TObject>(string varPath, TObject varObject, string error) where TObject : Object;
    /// <summary>
    /// 加载资源回调
    /// </summary>
    /// <param name="varPath"></param>
    /// <param name="varObject"></param>
    /// <param name="error"></param>
    public delegate void LoadResourcesCallback(string varPath, UnityEngine.Object varObject, string error);
    /// <summary>
    /// 加载AssetBundle完成
    /// </summary>
    /// <param name="asset"></param>
    public delegate void LoadResourcesFinish<TObject>(TObject asset) where TObject : Object;
    /// <summary>
    /// 加载场景结果
    /// </summary>
    /// <param name="path"></param>
    /// <param name="result"></param>
    public delegate void LoadSceneFinish(string path, string error);

}
