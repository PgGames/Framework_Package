using System.IO;
using UnityEditor;
using UnityEngine;


namespace PGFrammework.PGEditor
{
    public class CommonUI
    {
        public const float Tools_barPadding = 15f;
        public const float Tools_Buttonspacing = 5f;


        public static string GUI_SelectDirectory(string varPath, string varTitle)
        {
            EditorGUILayout.BeginHorizontal();
            varPath = EditorGUILayout.TextField(varTitle, varPath);
            if (GUILayout.Button("Choose..", GUILayout.Width(100)))
            {
                varPath = EditorUtility.OpenFolderPanel("选择文件夹", varPath, "");
            }
            if (GUILayout.Button("Open File", GUILayout.Width(100)))
            {
                if (string.IsNullOrEmpty(varPath) || !Directory.Exists(varPath))
                {
                    Application.OpenURL(Application.dataPath);
                }
                else
                {
                    Application.OpenURL(varPath);
                }
            }
            EditorGUILayout.EndHorizontal();
            return varPath;
        }
    }
}