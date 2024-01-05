
namespace PGFrammework.Res
{
    /// <summary>
    /// 加载AssetBundle回调
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="varPath"></param>
    /// <param name="varObject"></param>
    public delegate void LoadResourcesCallback(string varPath, UnityEngine.Object varObject, string error);
    /// <summary>
    /// 加载AssetBundle完成
    /// </summary>
    /// <param name="asset"></param>
    public delegate void LoadResourcesFinish(UnityEngine.Object asset);

}
