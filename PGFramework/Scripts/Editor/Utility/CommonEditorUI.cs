using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;


namespace PGFrammework.Editor
{

    public class CommonEditorUI
    {
        public const string NoneOptionName = "<None>";
        private const float Tools_barPadding = 15f;
        private const float Tools_Buttonspacing = 5f;

        /// <summary>
        /// GUI-辅助器选择栏
        /// </summary>
        /// <param name="GUIName">GUI 的显示名称</param>
        /// <param name="varSerialized">属性参数</param>
        /// <param name="varHelperTypeNames">辅助器列表</param>
        /// <param name="Index">当前ID</param>
        public static void GUI_Helper(string GUIName, string stringValue, string[] varHelperTypeNames, ref int Index)
        {
            int tempHelperSelectedIndex = EditorGUILayout.Popup(GUIName, Index, varHelperTypeNames);
            if (tempHelperSelectedIndex != Index)
            {
                Index = tempHelperSelectedIndex;
                stringValue = tempHelperSelectedIndex <= 0 ? null : varHelperTypeNames[tempHelperSelectedIndex];
            }
        }

        /// <summary>
        /// 获取辅助器列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="varSerialized"></param>
        /// <param name="varHelperTypeNames"></param>
        /// <param name="varHelperTypeNameIndex"></param>
        public static void GetHelperList<T>(string stringValue, out string[] varHelperTypeNames, out int varHelperTypeNameIndex)
        {
            List<string> tempHelperTypeNames = new List<string>
            {
                NoneOptionName
            };
            tempHelperTypeNames.AddRange(Type.GetRuntimeTypeNames(typeof(T)));
            varHelperTypeNames = tempHelperTypeNames.ToArray();
            varHelperTypeNameIndex = 0;
            if (!string.IsNullOrEmpty(stringValue))
            {
                varHelperTypeNameIndex = tempHelperTypeNames.IndexOf(stringValue);
                if (varHelperTypeNameIndex <= 0)
                {
                    varHelperTypeNameIndex = 0;
                    stringValue = null;
                }
            }
        }

        /// <summary>
        /// 选择文件夹
        /// </summary>
        /// <param name="varPath"></param>
        /// <param name="varTitle"></param>
        /// <returns></returns>
        public static string GUI_SelectDirectory(string varPath, string varTitle)
        {
            EditorGUILayout.BeginHorizontal();
            varPath = EditorGUILayout.TextField(varTitle, varPath);
            if (GUILayout.Button("Choose..", GUILayout.Width(100)))
            {
                string tempPath = null;
                if (!string.IsNullOrEmpty(varPath))
                {
                    tempPath = EditorUtility.OpenFolderPanel("选择文件夹", varPath, string.Empty);
                }
                else
                {
                    tempPath = EditorUtility.OpenFolderPanel("选择文件夹", Application.dataPath, string.Empty);
                }
                if (!string.IsNullOrEmpty(tempPath))
                {
                    varPath = tempPath;
                }
            }
            EditorGUILayout.EndHorizontal();
            return varPath;
        }
        /// <summary>
        /// 选择文件
        /// </summary>
        /// <param name="varPath"></param>
        /// <param name="varTitle"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static string GUI_SelectFilePanel(string varPath, string varTitle, string extension)
        {
            EditorGUILayout.BeginHorizontal();
            varPath = EditorGUILayout.TextField(varTitle, varPath);
            if (GUILayout.Button("Choose..", GUILayout.Width(100)))
            {
                varPath = EditorUtility.OpenFilePanel("选择文件", varPath, extension);
            }
            if (GUILayout.Button("Open File", GUILayout.Width(100)))
            {
                string tempDirectory = Path.GetDirectoryName(varPath);
                if (!Directory.Exists(tempDirectory))
                {
                    Directory.CreateDirectory(tempDirectory);
                }
                Application.OpenURL(tempDirectory);
            }
            EditorGUILayout.EndHorizontal();
            return varPath;
        }

        /// <summary>
        /// 枚举按钮
        /// 
        ///     15px的边距
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="varEnum">枚举值</param>
        /// <param name="width">总宽度</param>
        /// <param name="options">按钮属性</param>
        /// <returns></returns>
        public static bool EnumButton<T>(ref T varEnum, /*float width, */params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            string[] tringname = System.Enum.GetNames(varEnum.GetType());
            int count = tringname.Length;
            GUILayout.Space(Tools_barPadding);
            for (int i = 0; i < count; i++)
            {
                string tempName = tringname[i];
                if (GUILayout.Button(tempName, options))
                {
                    object temp = (System.Enum.Parse(varEnum.GetType(), tempName));
                    varEnum = (T)temp;
                    return true;
                }
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            return false;
        }
        /// <summary>
        /// 居中按钮
        /// </summary>
        /// <param name="text">按钮文字</param>
        /// <param name="options">按钮属性</param>
        /// <returns></returns>
        public static bool CenterButton(string text, params GUILayoutOption[] options)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button(text, options))
            {
                return true;
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            return false;
        }
        /// <summary>
        /// 居中按钮
        /// </summary>
        /// <param name="text"></param>
        /// <param name="index"></param>
        /// <param name="options">按钮属性</param>
        /// <returns></returns>
        public static bool CenterButton(string[] text, out int index, params GUILayoutOption[] options)
        {
            index = 0;
            bool onclick = false;
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            string[] tringname = text;
            int count = tringname.Length;

            GUILayout.Space(Tools_barPadding);
            for (int i = 0; i < count; i++)
            {
                string tempName = tringname[i];
                if (GUILayout.Button(tempName, options))
                {
                    index = i;
                    onclick = true;
                }
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            return onclick;
        }


        /// <summary>
        /// 居中文字
        /// </summary>
        /// <param name="text"></param>
        /// <param name="options"></param>
        public static void CenterLabel(string text, params GUILayoutOption[] options)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(text, options);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }


        /// <summary>
        /// 右对齐按钮
        /// </summary>
        /// <param name="text"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static bool RightButton(string text, params GUILayoutOption[] options)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button(text, options))
            {
                return true;
            }
            GUILayout.EndHorizontal();
            return false;
        }

    }
}