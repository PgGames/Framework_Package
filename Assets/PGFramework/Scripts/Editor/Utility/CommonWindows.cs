using PGFrammework.Runtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;


namespace PGFrammework.PGEditor
{
    public class CommonWindow 
    {
        public const string Tools = "PGFrammework/Tools";


        /// <summary>
        ///	读取数据
        /// </summary>
        public static T ReadDate<T>() where T : BaseDate, new()
        {
            string path = typeof(T).Name;
            T date = null;
            var dataPath = System.IO.Path.GetFullPath(".");
            dataPath = dataPath.Replace("\\", "/");
            dataPath += "/Library/FrameworkConfig/Tools_" + path + ".dat";
            //读取数据
            if (File.Exists(dataPath))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.OpenRead(dataPath);
                try
                {
                    date = bf.Deserialize(file) as T;
                    file.Close();
                }
                catch
                {
                    file.Close();
                    Debug.LogError("Read dat error on path :" + dataPath);
                    File.Delete(dataPath);
                }
            }
            if (date == null)
            {
                date = new T();
                date.Init();
            }
            return date;
        }
        /// <summary>
        /// 存储数据
        /// </summary>
        public static void SaveDate<T>(T date) where T : BaseDate, new()
        {
            string path = typeof(T).Name;
            var dataPath = System.IO.Path.GetFullPath(".");
            dataPath = dataPath.Replace("\\", "/");
            dataPath += "/Library/FrameworkConfig/Tools_" + path + ".dat";
            //判断文件是否存在
            if (!File.Exists(dataPath))
            {
                if (!Directory.Exists(Path.GetDirectoryName(dataPath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(dataPath));
                }
            }
            //存储数据
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenWrite(dataPath);
            try
            {
                bf.Serialize(file, date);
                file.Close();
            }
            catch
            {
                file.Close();
                Debug.LogError("Save dat error on path :" + dataPath);
            }
        }

        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="directory">文件夹路径</param>
        /// <param name="varExtension">特定文件后缀名</param>
        /// <returns></returns>
        public static FileInfo[] ReadFile(DirectoryInfo directory, params string[] varExtension)
        {
            List<FileInfo> TempFiles = new List<FileInfo>();
            FileInfo[] files = directory.GetFiles();
            if (varExtension != null && files != null)
            {
                for (int j = 0; j < files.Length; j++)
                {
                    var tempfile = files[j];
                    if (varExtension.Contains(tempfile.Extension))
                    {
                        TempFiles.Add(tempfile);
                    }
                }
            }
            else if (files != null)
            {
                TempFiles.AddRange(files);
            }
            if (directory.GetDirectories() != null)
            {
                DirectoryInfo[] directories = directory.GetDirectories();
                for (int i = 0; i < directories.Length; i++)
                {
                    var tempfiles = ReadFile(directories[i], varExtension);
                    if (tempfiles != null)
                        TempFiles.AddRange(tempfiles);
                    //刷新进度避免卡死
                    EditorUtility.DisplayProgressBar("Read load file", directory.FullName, (i * 1.0f) / directories.Length);
                }
            }
            return TempFiles.ToArray();
        }
        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="directory">文件夹路径</param>
        /// <param name="varExtension">忽略文件后缀名</param>
        /// <returns></returns>
        public static FileInfo[] ReadFileExtension(DirectoryInfo directory, params string[] varExtension)
        {
            List<FileInfo> TempFiles = new List<FileInfo>();
            FileInfo[] files = directory.GetFiles();
            if (varExtension != null && files != null)
            {
                for (int j = 0; j < files.Length; j++)
                {
                    var tempfile = files[j];
                    if (!varExtension.Contains(tempfile.Extension))
                    {
                        TempFiles.Add(tempfile);
                    }
                }
            }
            else if (files != null)
            {
                TempFiles.AddRange(files);
            }
            if (directory.GetDirectories() != null)
            {
                DirectoryInfo[] directories = directory.GetDirectories();
                for (int i = 0; i < directories.Length; i++)
                {
                    var tempfiles = ReadFileExtension(directories[i], varExtension);
                    if (tempfiles != null)
                        TempFiles.AddRange(tempfiles);
                    //刷新进度避免卡死
                    EditorUtility.DisplayProgressBar("Read load file", directory.FullName, (i * 1.0f) / directories.Length);
                }
            }
            return TempFiles.ToArray();
        }
        /// <summary>
        /// 设置进度显示
        /// </summary>
        public static void DisplayProgressBar(string varTitle, string varContent, float varProgress)
        {
            EditorUtility.DisplayProgressBar(varTitle, varContent, varProgress);
            if (varProgress >= 1)
            {
                EditorUtility.ClearProgressBar();
            }
        }
    }
    //必须添加System.Serializable的属性否则无法进行反射
    [System.Serializable]
    public class BaseDate
    {
        public virtual void Init()
        {
        }
    }
}