using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGFrammework.Res
{
    public interface IResourse
    {
        /// <summary>
        /// 加载资源
        /// </summary>
        /// <param name="varPath"></param>
        /// <param name="Callback"></param>
        void LoadAssets(string varPath, LoadResourcesCallback Callback);
        /// <summary>
        /// 加载场景
        /// </summary>
        /// <param name="varPath"></param>
        /// <param name="Callback"></param>
        void LoadScene(string varPath, LoadResourcesCallback Callback);
    }
}
