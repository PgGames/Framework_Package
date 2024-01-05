using System;
using System.Collections.Generic;
using PGFrammework.UI;

namespace PGFrammework.Runtime
{
    /// <summary>
    /// 加载UI成功回调
    /// </summary>
    /// <param name="uIBase"></param>
    public delegate void LoadUISuccess(UICanvasBase uIBase);
    /// <summary>
    /// 加载UI失败回调
    /// </summary>
    /// <param name="error"></param>
    public delegate void LoadUIFail(string error);
}
