using UnityEditor;
using UnityEngine;

namespace PGFrammework.PGEditor
{
    public enum AddressableType
    {
        /// <summary>
        /// 
        /// </summary>
        ResourcesFile,
        /// <summary>
        /// 
        /// </summary>
        ResourcesFolder,
    }
    public enum GroupType
    {
        /// <summary>
        /// 文件夹
        /// </summary>
        Folder,
        /// <summary>
        /// 文件类型
        /// </summary>
        FileType,
    }
    public enum AssetNameType
    {
        /// <summary>
        /// Assets路径
        /// </summary>
        Assets,
        /// <summary>
        /// 资源根目录的路径
        /// </summary>
        Root,
        /// <summary>
        /// 资源根目录的相对路径
        /// </summary>
        RootLocal,
        /// <summary>
        /// 文件名称
        /// </summary>
        FileName,
    }
}