using UnityEditor;
using UnityEngine;

namespace PGFrammework.PGEditor
{
    [System.Serializable]
    public class AddressWindowData : BaseDate
    {
        public string[] m_ResourceFolder;
        public string[] m_IgnoreFolder;
        public string[] m_IgnoreFile;

        /// <summary>
        /// 资源类型
        /// </summary>
        public int m_addressType;
        /// <summary>
        /// 资源归类类型
        /// </summary>
        public int m_GroupType;
        /// <summary>
        /// 资源名称类型
        /// </summary>
        public int m_AssetNameType;

        public override void Init()
        {
            m_ResourceFolder = new string[] { };
            m_IgnoreFolder = new string[] { };
            m_IgnoreFile = new string[] { };
            m_addressType = 0;
            m_GroupType = 1;
            m_AssetNameType = 1;
        }
    }


    public class EnumWindowData<T> where T: System.Enum
    {
        public T enumValue;
        public string[] tips;
        public GUIStyle[] styles;
    }
}