
namespace PGFrammework.Runtime
{
    public interface IResourcesComponent
    {
        /// <summary>
        /// 异步加载资源
        /// ps：
        ///     以队列的模式加载资源，一个资源加载完成在去加载下一个资源
        /// </summary>
        /// <param name="assetsName">资源名称</param>
        /// <param name="loadAsset">加载回调</param>
        void AsynLoadAsset(string assetsName, LoadAssetCallbacks loadAsset);
        /// <summary>
        /// 异步加载资源
        /// ps：
        ///     以队列的模式加载资源，一个资源加载完成在去加载下一个资源
        /// </summary>
        /// <param name="assetsName">资源名称</param>
        /// <param name="priority">加载的优先级</param>
        /// <param name="loadAsset">加载回调</param>
        void AsynLoadAsset(string assetsName, int priority, LoadAssetCallbacks loadAsset);
        /// <summary>
        /// 异步加载场景
        /// ps：
        ///     以队列的模式加载资源，一个资源加载完成在去加载下一个资源
        /// </summary>
        /// <param name="assetsName">资源名称</param>
        /// <param name="loadAsset">加载回调</param>
        void AsynLoadScene(string assetsName, LoadAssetCallbacks loadAsset);
        /// <summary>
        /// 异步加载场景
        /// ps：
        ///     以队列的模式加载资源，一个资源加载完成在去加载下一个资源
        /// </summary>
        /// <param name="assetsName">资源名称</param>
        /// <param name="priority">加载的优先级</param>
        /// <param name="loadAsset">加载回调</param>
        void AsynLoadScene(string assetsName, int priority, LoadAssetCallbacks loadAsset);
    }
}
