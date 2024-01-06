using System;
using System.Collections.Generic;
using PGFrammework.UI;

namespace PGFrammework.Runtime
{
    /// <summary>
    /// 加载UI结果
    /// </summary>
    /// <param name="uibase">UI界面</param>
    /// <param name="error">日志</param>
    /// <param name="userdata">自定义数据</param>
    public delegate void LoadUIResult(UICanvasBase uibase, string error, object userdata);
    /// <summary>
    /// 加载UI成功回调
    /// </summary>
    /// <param name="uIBase">UI界面</param>
    /// <param name="usedata">自定义数据</param>
    public delegate void LoadUISuccess(UICanvasBase uIBase,object usedata);
    /// <summary>
    /// 加载UI失败回调
    /// </summary>
    /// <param name="error">日志</param>
    /// <param name="usedata">自定义数据</param>
    public delegate void LoadUIFail(string error,object usedata);
}
