using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGFrammework.UI
{
    public struct UIInfo
    {
        private string m_AssetsName;
        private int m_GroupDepth;
        private int m_UIDepth;

        public UIInfo(string assetsMame, int groupDepth)
        {
            m_AssetsName = assetsMame;
            m_GroupDepth = groupDepth;
            m_UIDepth = 0;
        }

        /// <summary>
        /// UI资源名称
        /// </summary>
        public string AssetsName { get => m_AssetsName; }
        /// <summary>
        /// UI组深度
        /// </summary>
        public int GroupDepth { get => m_GroupDepth; }
        /// <summary>
        /// UI组内深度
        /// </summary>
        public int UIDepth { get => m_UIDepth; set => m_UIDepth = value; }
    }
}
