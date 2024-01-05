using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PGFrammework.Res
{
    public class AssetBundleConfig
    {
        /// <summary>
        /// AssetBundle资源后缀
        /// </summary>
        public const string AssetBundleVariant = "bundle";
        /// <summary>
        /// AssetBundle资源前缀
        /// </summary>
        public const string AssetBundlePrefix = "assets/";
        /// <summary>
        /// AssetBundle依赖
        /// </summary>
        public const string AssetBundleManifest = "AssetBundleManifest";

        /// <summary>
        /// AssetBundle资源路径
        /// </summary>
        public static string AssetBundlePath { get { return string.Format("{0}/{1}", Application.streamingAssetsPath, Platform); } }



        /// <summary>
        /// 资源平台
        /// </summary>
#if UNITY_STANDALONE_WIN
        public const string Platform = "Windows";
#elif UNITY_ANDROID
        public const string Platform = "Android";
#elif UNITY_IOS || UNITY_IPHONE
        public const string Platform = "IOS";
#else
        public const string Platform = "Windows";
#endif

    }
}
