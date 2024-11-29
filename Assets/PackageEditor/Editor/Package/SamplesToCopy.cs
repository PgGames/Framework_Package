using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SamplesToCopy : EditorWindow
{
    [MenuItem("Package Tools/Samples To Copy")]
    public static void ShowWindows()
    {
        SamplesToCopy samplesToCopy = EditorWindow.GetWindow<SamplesToCopy>();
        samplesToCopy.titleContent = new GUIContent(typeof(SamplesToCopy).Name);
        samplesToCopy.Show();
    }

    private Vector2 mRect;
    private string copyPath;
    private string toPath;


    private void OnGUI()
    {
        mRect = EditorGUILayout.BeginScrollView(mRect);
        UI_GUI();
        EditorGUILayout.EndScrollView();
    }
    private void UI_GUI()
    {
        copyPath = CommonUI.GUI_SelectDirectory(copyPath, "Copy Folder");
        toPath = CommonUI.GUI_SelectDirectory(toPath, "To Folder");

        if (string.IsNullOrEmpty(copyPath))
        {
            copyPath = CommonFunc.GetFolder("Samples");
        }
        if (string.IsNullOrEmpty(toPath))
        {
            toPath = CommonFunc.GetFolder("Samples~");
            if (!Directory.Exists(toPath))
            {
                string json = CommonFunc.GetFile("package.json");
                if (!string.IsNullOrEmpty(json))
                {
                    string dir = Path.GetDirectoryName(json);
                    toPath = $"{dir.Replace("\\","/")}/Samples~";
                }
            }
        }

        if (GUILayout.Button("Copy"))
        {
            CommonFunc.CopyFolder(copyPath, toPath);
            EditorUtility.ClearProgressBar();
            AssetDatabase.Refresh();
            Debug.Log($"Copy file success");
        }

    }


}
