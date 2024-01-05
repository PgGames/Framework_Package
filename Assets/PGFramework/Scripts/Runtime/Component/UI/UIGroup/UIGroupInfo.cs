using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGFrammework.UI
{
    [System.Serializable]
    public class UIGroupInfo
    {
        public string GroupName;
        public int Depth;



        public static UIGroupInfo GetInfo(string varGroupName, int varDepth)
        {
            UIGroupInfo info = new UIGroupInfo();
            info.GroupName = varGroupName;
            info.Depth = varDepth;

            return info;
        }
    }
}
