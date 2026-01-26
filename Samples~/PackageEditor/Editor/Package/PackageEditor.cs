using PGFrammework.PGEditor;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;



namespace PGFrammework.PackageEditor
{
    public class PackageEditor : EditorWindow
    {
        [MenuItem("Package Tools/Json Editor")]
        public static void ShowWindows()
        {
            PackageEditor packageEditor = EditorWindow.GetWindow<PackageEditor>();
            packageEditor.titleContent = new UnityEngine.GUIContent(typeof(PackageEditor).Name);
            packageEditor.Show();
        }
        protected Vector2 mRect { set; get; }
        /// <summary>
        /// json路径
        /// </summary>
        private string jsonPath { set; get; }
        /// <summary>
        /// json数据
        /// </summary>
        private PackageData m_jsonData { set; get; }

        private Dictionary<string, string> m_Dependencies;

        private void OnGUI()
        {
            mRect = EditorGUILayout.BeginScrollView(mRect);
            UI_GUI();
            EditorGUILayout.EndScrollView();
        }
        private void UI_GUI()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Box("Package");
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space();
            if (string.IsNullOrEmpty(jsonPath) || !File.Exists(jsonPath) || m_jsonData == null || m_Dependencies == null)
            {
                ReadJson();
            }
            if (m_jsonData != null)
            {
                GUI_TextField("name:(只能用小写字母)", ref m_jsonData.name);
                m_jsonData.name = m_jsonData.name.ToLower();    //包名不支持大写
                GUI_TextField("version:", ref m_jsonData.version);
                GUI_TextField("displayName:", ref m_jsonData.displayName);
                GUI_TextArea("description:", ref m_jsonData.description);
                GUI_TextField("unity:", ref m_jsonData.unity);

                GUI_Show_Dependencies();
                GUI_Show_Keywords();
                GUI_Show_Samples();
                GUI_Show_Author();


                GUILayout.BeginHorizontal();
                GUILayout.BeginVertical();
                GUILayout.FlexibleSpace();
                GUILayout.Box("JsonPath");
                GUILayout.FlexibleSpace();
                GUILayout.EndVertical();

                GUILayout.BeginVertical();
                GUILayout.FlexibleSpace();
                GUILayout.Label(jsonPath);
                GUILayout.FlexibleSpace();
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();


                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Save"))
                {
                    string json = JsonUtility.ToJson(m_jsonData);
                    json = json.Replace("\"keywords\"", $"{DependenciesToJson()},\"keywords\"");

                    File.WriteAllText(jsonPath, json);
                    Debug.Log("save success");
                    AssetDatabase.Refresh();
                }
                if (GUILayout.Button("Save as"))
                {
                    string json = JsonUtility.ToJson(m_jsonData);
                    json = json.Replace("\"keywords\"", $"{DependenciesToJson()},\"keywords\"");

                    string path = EditorUtility.OpenFilePanel("选择文件", jsonPath, "json");
                    if (File.Exists(path))
                    {
                        jsonPath = path;
                        File.WriteAllText(jsonPath, json);
                        Debug.Log("save success");
                        AssetDatabase.Refresh();
                    }
                }
                if (GUILayout.Button("↑↓", GUILayout.Width(30)))
                {
                    ReadJson();
                }
                GUILayout.EndHorizontal();
            }
            else
            {
                GUILayout.Label("项目中为找到package.json文件");
            }
            EditorGUILayout.EndVertical();
        }
        private void GUI_TextField(string name, ref string varlabel)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(name);
            varlabel = EditorGUILayout.TextField(varlabel);
            GUILayout.EndHorizontal();
        }
        private void GUI_TextArea(string name, ref string varlabel)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(name);
            varlabel = EditorGUILayout.TextArea(varlabel);
            GUILayout.EndHorizontal();
        }
        private void GUI_Show_Dependencies()
        {
            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.BeginHorizontal();
            List<string[]> strings = new List<string[]>();
            EditorGUILayout.LabelField("dependencies");
            if (m_Dependencies != null)
            {
                var date = m_Dependencies;
                foreach (var item in date)
                {
                    strings.Add(new string[] { item.Key, item.Value });
                }
            }
            if (GUILayout.Button("+"))
            {
                strings.Add(new string[] { "", "" });
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            for (int i = 0; i < strings.Count; i++)
            {
                strings[i][0] = GUILayout.TextField(strings[i][0]);
            }
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            for (int i = 0; i < strings.Count; i++)
            {
                strings[i][1] = GUILayout.TextField(strings[i][1]);
            }
            GUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            m_Dependencies = new Dictionary<string, string>();
            foreach (var item in strings)
            {
                string key = item[0];
                string value = item[1];

                if (m_Dependencies.ContainsKey(key))
                {
                    m_Dependencies[key] = value;
                }
                else
                {
                    m_Dependencies.Add(key, value);
                }
            }
            EditorGUILayout.EndVertical();
        }
        private void GUI_Show_Keywords()
        {
            EditorGUILayout.BeginVertical("box");
            List<string> list = new List<string>();
            if (m_jsonData.keywords != null)
            {
                list.AddRange(m_jsonData.keywords);
            }

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("keywords");
            if (GUILayout.Button("+"))
            {
                list.Add("");
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            for (int i = 0; i < list.Count; i++)
            {
                GUILayout.BeginHorizontal();
                list[i] = GUILayout.TextField(list[i]);
                if (GUILayout.Button("-", GUILayout.Width(30)))
                {
                    list.RemoveAt(i);
                    i--;
                }
                GUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
            m_jsonData.keywords = list.ToArray();
        }
        private void GUI_Show_Samples()
        {
            EditorGUILayout.BeginVertical("box");
            List<Samples> list = new List<Samples>();
            if (m_jsonData.samples != null)
            {
                list.AddRange(m_jsonData.samples);
            }

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("samples");
            if (GUILayout.Button("+"))
            {
                list.Add(new Samples());
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            for (int i = 0; i < list.Count; i++)
            {
                GUILayout.BeginHorizontal();
                {
                    GUILayout.Space(20);
                    EditorGUILayout.BeginVertical("box");
                    {
                        int index = i;
                        {
                            GUILayout.BeginHorizontal();
                            if (GUILayout.Button("-", GUILayout.Width(30)))
                            {
                                list.RemoveAt(i);
                                i--;
                            }
                            GUILayout.FlexibleSpace();
                            GUILayout.EndHorizontal();
                        }
                        if (i == index)
                        {
                            Samples samples = list[i];
                            GUI_TextField("displayName", ref samples.displayName);
                            GUI_TextField("description", ref samples.description);
                            GUI_TextField("path", ref samples.path);
                            list[i] = samples;
                        }
                    }
                    EditorGUILayout.EndVertical();
                }
                GUILayout.EndHorizontal();

            }
            EditorGUILayout.EndVertical();
            m_jsonData.samples = list.ToArray();
        }
        private void GUI_Show_Author()
        {
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField("Author");
            if (m_jsonData.author != null)
            {
                GUI_TextField("name:", ref m_jsonData.author.name);
                GUI_TextField("email:", ref m_jsonData.author.email);
                GUI_TextField("url:", ref m_jsonData.author.url);
            }
            EditorGUILayout.EndVertical();
        }



        private void ReadJson()
        {
            jsonPath = CommonFunc.GetFile("package.json");
            string jsonstr = File.ReadAllText(jsonPath);
            m_jsonData = JsonUtility.FromJson<PackageData>(jsonstr);

            AnalysisDependencies(jsonstr);
        }

        private void AnalysisDependencies(string json)
        {
            int index = json.IndexOf("dependencies");
            if (index < 0)
                return;
            string str = json.Substring(index);
            int startIndex = str.IndexOf('{');
            int endIndex = str.IndexOf('}');

            string content = str.Substring(startIndex + 1, endIndex - startIndex - 1);
            string[] array = content.Split(',');
            m_Dependencies = new Dictionary<string, string>();


            foreach (var item in array)
            {
                string[] strings = item.Split(':');
                string key = RemoveNull(strings[0]);
                string value = RemoveNull(strings[1]);

                if (m_Dependencies.ContainsKey(key))
                {
                    m_Dependencies[key] = value;
                }
                else
                {
                    m_Dependencies.Add(key, value);
                }
            }
        }
        private string RemoveNull(string str)
        {
            str = str.Trim();
            str = str.Trim('\r');
            str = str.Trim('\n');
            str = str.Trim('\r');
            str = str.Trim('\"');
            return str;
        }


        private string DependenciesToJson()
        {
            string str = null;
            foreach (var item in m_Dependencies)
            {
                if (str != null)
                {
                    str += ",";
                }
                str += $"\"{item.Key.Trim(',')}\":\"{item.Value.Trim(',')}\"";
            }
            return $"\"dependencies\":{{{str}}}";
        }
    }

}