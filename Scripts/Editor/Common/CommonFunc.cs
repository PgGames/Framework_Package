using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace PGFrammework.PGEditor
{
    public class CommonFunc
    {
        /// <summary>
        /// 获取文件夹
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public static string GetFolder(string folder)
        {
            string[] files = Directory.GetDirectories(Application.dataPath, folder, SearchOption.AllDirectories);
            string packageFolder = null;
            if (files.Length > 0)
            {
                string path = files[0];
                packageFolder = path.Replace('\\', '/');
            }
            return packageFolder;
        }
        /// <summary>
        /// 获取文件夹
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public static string GetFolderByFile(string file)
        {
            string[] files = Directory.GetFiles(Application.dataPath, file, SearchOption.AllDirectories);
            string packageFolder = null;
            if (files.Length > 0)
            {
                string path = files[0];
                packageFolder = Path.GetDirectoryName(path);
            }
            return packageFolder;
        }
        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFile(string fileName)
        {
            string[] files = Directory.GetFiles(Application.dataPath, fileName, SearchOption.AllDirectories);
            string packageFolder = null;
            if (files.Length > 0)
            {
                string path = files[0];
                packageFolder = path.Replace('\\', '/');
            }
            return packageFolder;
        }
        /// <summary>
        /// 复制文件夹
        /// </summary>
        /// <param name="copyFolder"></param>
        /// <param name="toFolder"></param>
        public static void CopyFolder(string copyFolder, string toFolder)
        {
            if (!Directory.Exists(toFolder))
                Directory.CreateDirectory(toFolder);
            if (!Directory.Exists(copyFolder))
                return;
            //拷贝文件
            string[] directoryInfos = Directory.GetDirectories(copyFolder);
            foreach (var item in directoryInfos)
            {
                string path = item.Replace(copyFolder, "");
                CopyFolder(item, $"{toFolder}/{path}");
            }
            //拷贝文件夹
            string[] files = Directory.GetFiles(copyFolder);

            for (int i = 0; i < files.Length; i++)
            {
                string fileInfo = files[i];
                string fileName = Path.GetFileName(fileInfo);
                string tofile = $"{toFolder}/{fileName}";
                if (File.Exists(tofile))
                {
                    File.Delete(tofile);
                }
                File.Copy(fileInfo, tofile);
                EditorUtility.DisplayProgressBar("Copy Folder", $"copy:{toFolder}", i / files.Length);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        public static string GetRoot(string folder, string root)
        {
            string[] directories = Directory.GetDirectories(folder);
            for (int i = 0; i < directories.Length; i++)
            {
                string loadpath = directories[i];
                loadpath = loadpath.Replace("\\", "/");
                int index = loadpath.LastIndexOf('/');
                string dirname = loadpath.Substring(index + 1);
                if (dirname.Equals(root))
                {
                    return loadpath.Substring(0, index);
                }
            }
            string path = folder.Replace("\\", "/");
            int endindex = path.LastIndexOf('/');
            if (endindex < 0)
                return null;
            path = path.Substring(0, endindex);
            return GetRoot(path, root);
        }
    }

}