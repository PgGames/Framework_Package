using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGFrammework
{
    /// <summary>
    /// 加载表格回调
    /// </summary>
    /// <param name="success">成功或失败</param>
    /// <param name="error">错误数据</param>
    /// <param name="userData">用户自定义数据</param>
    public delegate void LoadDataTableCall(bool success, string error, object userData);
}
