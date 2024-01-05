using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGFrammework.Runtime
{
    public interface IUIComponent
    {
        /// <summary>
        /// 添加UI组
        /// </summary>
        /// <param name="varGroupName"></param>
        /// <param name="varDepth"></param>
        void AddGroup(string varGroupName, int varDepth);
        /// <summary>
        /// 打开UI
        /// </summary>
        /// <param name="assetsName"></param>
        /// <param name="varDepth"></param>
        void OpenUI(string assetsName, int varDepth);
    }
}
