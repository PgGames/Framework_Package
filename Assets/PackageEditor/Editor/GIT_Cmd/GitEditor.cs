using System.IO;
using UnityEditor;
using UnityEngine;

public class GitEditor : EditorWindow
{
    [MenuItem("Package Tools/Package Git")] 
    public static void ShowWindows()
    {
        GitEditor gitEditor = EditorWindow.GetWindow<GitEditor>();
        gitEditor.titleContent = new GUIContent(typeof(GitEditor).Name);
        gitEditor.Show();
    }
    private Vector2 mRect;
    private string packageFolder;
    private string version;
    private string loadpath;
    private string m_Root;


    private void OnGUI()
    {
        mRect = EditorGUILayout.BeginScrollView(mRect);

        UI_GUI();

        EditorGUILayout.EndScrollView();
    }


    private void UI_GUI()
    {
        string packFolder = CommonUI.GUI_SelectDirectory(packageFolder,"Package Folder");
        if(string.IsNullOrEmpty(packageFolder))
        {
            packFolder = CommonFunc.GetFolderByFile("package.json");
        }
        if (packFolder != packageFolder)
        {
            UpDatePackage(packFolder);
        }

        EditorGUILayout.LabelField($"git Working Directory: {m_Root}");

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Open Git Bash"))
        {
            ExecuteGitCommand();
        }
        if (GUILayout.Button("↑↓", GUILayout.Width(30)))
        {
            UpDatePackage(packFolder);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.LabelField("step 1:将Package包文件及单独分割至upm分支");
        EditorGUILayout.LabelField("tips：提交分支的操作都是于当前分支提交的内容为基准进行分割的，分割前请确保修改内容已提交至主分支");
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("copy git 命令"))
        {
            GUIUtility.systemCopyBuffer = $"git subtree split --prefix={loadpath} --branch upm";
        }
        EditorGUILayout.LabelField($"git subtree split --prefix={loadpath} --branch upm");
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.LabelField("step 2:给upm分支添加版本标签");
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("copy git 命令"))
        {
            GUIUtility.systemCopyBuffer = $"git tag {version} upm";
        }
        EditorGUILayout.LabelField($"git tag {version} upm");
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.LabelField("step 3:将标签信息推送至服务端");
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("copy git 命令"))
        {
            GUIUtility.systemCopyBuffer = $"git push origin upm --tags";
        }
        EditorGUILayout.LabelField($"git push origin upm --tags");
        EditorGUILayout.EndHorizontal();

    }
    private void UpDatePackage(string url)
    {
        if (File.Exists($"{url}/package.json"))
        {
            packageFolder = url;

            string root = CommonFunc.GetRoot(url, ".git");
            if (!string.IsNullOrEmpty(root))
            {
                m_Root = root.Replace("\\", "/");
                m_Root = m_Root.Trim('/');

                loadpath = url.Remove(0, root.Length);
                loadpath = loadpath.Replace("\\", "/");
                loadpath = loadpath.Trim('/');
            }

            string content = File.ReadAllText($"{url}/package.json");
            var data = JsonUtility.FromJson<PackageData>(content);
            if (data != null)
            {
                version = data.version;
            }
        }
    }


    private void ExecuteGitCommand()
    {
        //设置命令行参数
        System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
        psi.FileName= $"C:\\Program Files\\Git\\git-bash.exe";
        psi.WorkingDirectory = m_Root;

        //
        System.Diagnostics.Process process = new System.Diagnostics.Process();
        process.StartInfo = psi;
        process.Start();
    }

}

