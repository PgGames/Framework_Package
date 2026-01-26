using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class PackageData
{
    /// <summary>
    /// 包名
    /// </summary>
    public string name;
    /// <summary>
    /// 版本号
    /// </summary>
    public string version;
    /// <summary>
    /// 名称
    /// </summary>
    public string displayName;
    /// <summary>
    /// 描述
    /// </summary>
    public string description;
    /// <summary>
    /// 最低unity版本
    /// </summary>
    public string unity;
    /// <summary>
    /// 搜索关键字
    /// </summary>
    public string[] keywords;
    /// <summary>
    /// demo示例
    /// </summary>
    public Samples[] samples;

    public Author author;

}
[System.Serializable]
public class Samples
{
    public string displayName;
    public string description;
    public string path;
}
[System.Serializable]
public class Author
{
    public string name;
    public string email;
    public string url;
}
