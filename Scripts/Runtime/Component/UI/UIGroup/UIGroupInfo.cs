using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGFrammework.UI
{
    [System.Serializable]
    public struct UIGroupInfo
    {
        public string m_GroupName;
        public int m_Depth;
        private bool m_RegularRecycling;
        private float m_RecoveryTimes;

        public string GroupName { get => m_GroupName; }
        public int Depth { get => m_Depth; }
        public bool RegularRecycling { get => m_RegularRecycling; set => m_RegularRecycling = value; }
        public float RecoveryTimes { get => m_RecoveryTimes; set => m_RecoveryTimes = value; }

        public static UIGroupInfo GetInfo(string varGroupName, int varDepth)
        {
            UIGroupInfo info = new UIGroupInfo();
            info.m_GroupName = varGroupName;
            info.m_Depth = varDepth;
            return info;
        }
    }
}
