using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using UnityEngine;

namespace PGFrammework.PGEditor
{
    public class AddressableWindows : EditorWindow
    {
        internal static AddressWindowData m_WindowsData;
        internal static GUIStyle m_Title;
        private static AddressableAssetSettings Settings
        {
            get
            {
                return AddressableAssetSettingsDefaultObject.Settings;
            }
        }


        [MenuItem(CommonWindow.Editor + "Resources/Addressable")]
        static void Windows()
        {
            AddressableWindows addressable = EditorWindow.GetWindow(typeof(AddressableWindows)) as AddressableWindows;
            addressable.titleContent = new GUIContent(typeof(AddressableWindows).Name);
            addressable.Show();
        }

        private void OnGUI()
        {
            if (m_WindowsData == null)
            {
                m_WindowsData = CommonWindow.ReadDate<AddressWindowData>();
                if (m_WindowsData.m_ResourceFolder == null)
                {
                    m_WindowsData.Init();
                }
            }
            if (GUI_ResourcesFolder("☛ 设置资源目录", ref m_WindowsData.m_ResourceFolder))
            {
                CommonWindow.SaveDate(m_WindowsData);
            }
            if (GUI_ResourcesFolder("☛ 设置忽略文件夹", ref m_WindowsData.m_IgnoreFolder))
            {
                CommonWindow.SaveDate(m_WindowsData);
            }
            if (GUI_IgnoreFile("☛ 设置忽略的文件", ref m_WindowsData.m_IgnoreFile))
            {
                CommonWindow.SaveDate(m_WindowsData);
            }
            if (GUI_BuilderType())
            {
                CommonWindow.SaveDate(m_WindowsData);
            }
            EditorGUILayout.Space();
            if (GUILayout.Button("Clear Group"))
            {
                ClearAssetGroup();
            }
            if (GUILayout.Button("Create Addressable Group"))
            {
                CreateAddressableGroup();
                RemoveEmptyGroups();
            }
        }

        private bool GUI_ResourcesFolder(string title, ref string[] varFolder)
        {
            List<string> array = new List<string>(varFolder);
            bool save = false;

            GUILayout.BeginVertical("Box");

            //EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(title, CommonGUIStyle.TitleStyle);
            if (GUILayout.Button("+"))
            {
                array.Add("");
                save = true;
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();

            for (int i = 0; i < array.Count; i++)
            {
                string url = array[i];
                url = url.Replace($"{Application.dataPath}/", "");
                EditorGUILayout.BeginHorizontal();

                string tempUrl = GUILayout.TextField(url);

                if (tempUrl != url)
                {
                    array[i] = tempUrl;
                    save = true;
                }


                if (GUILayout.Button("Choose..", GUILayout.Width(100)))
                {
                    string tempPath = null;
                    if (!string.IsNullOrEmpty(url))
                    {
                        tempPath = EditorUtility.OpenFolderPanel("选择文件夹", url, string.Empty);
                    }
                    else
                    {
                        tempPath = EditorUtility.OpenFolderPanel("选择文件夹", Application.dataPath, string.Empty);
                    }
                    if (!string.IsNullOrEmpty(tempPath))
                    {
                        array[i] = tempPath.Replace($"{Application.dataPath}/", "");
                        save = true;
                    }
                }
                if (GUILayout.Button("-", GUILayout.Width(20)))
                {
                    array.RemoveAt(i);
                    i--;
                    save = true;
                }
                EditorGUILayout.EndHorizontal();
            }


            GUILayout.EndVertical();

            varFolder = array.ToArray();
            return save;
        }

        private bool GUI_IgnoreFile(string title, ref string[] varFolder)
        {
            List<string> array = new List<string>(varFolder);
            bool save = false;

            GUILayout.BeginVertical("Box");

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(title, CommonGUIStyle.TitleStyle);
            if (GUILayout.Button("+"))
            {
                array.Add("");
                save = true;
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();

            for (int i = 0; i < array.Count; i++)
            {
                string url = array[i];
                EditorGUILayout.BeginHorizontal();
                string tempUrl = GUILayout.TextField(url, GUILayout.Width(200));
                GUILayout.Label($"*.{tempUrl}", GUILayout.Width(100));
                if (tempUrl != url)
                {
                    array[i] = tempUrl;
                    save = true;
                }

                if (GUILayout.Button("-", GUILayout.Width(20)))
                {
                    array.RemoveAt(i);
                    i--;
                    save = true;
                }
                EditorGUILayout.EndHorizontal();
            }

            GUILayout.Label("默认忽略 *.cs  *.dll 文件");

            GUILayout.EndHorizontal();
            varFolder = array.ToArray();
            return save;
        }

        private bool GUI_BuilderType()
        {
            bool save = false;

            EditorGUILayout.BeginVertical("box");

            GUILayout.Label("☛ 设置资源打包规则", CommonGUIStyle.TitleStyle);

            AddressableType addressType = (AddressableType)m_WindowsData.m_addressType;
            if (EnumUI<AddressableType>("资源打包模式", ref addressType, new EnumWindowData<AddressableType>[] {
                new EnumWindowData<AddressableType>(){ 
                    enumValue = AddressableType.ResourcesFile,
                    tips = new[]{ "❀ 使用资源目录为资源分类目录" }
                },
                new EnumWindowData<AddressableType>(){ 
                    enumValue = AddressableType.ResourcesFolder,
                    tips = new[]{ "❀ 使用资源目录下的文件夹为资源分类目录" ,"☛ 此设置会忽略资源目录下的直接文件！" },
                    styles = new GUIStyle[]{ null, CommonGUIStyle.WarningStyle }
                },
            }))
            {
                m_WindowsData.m_addressType = (int)addressType;
                save = true;
            }

            GroupType grouptype = (GroupType)m_WindowsData.m_GroupType;
            if (EnumUI<GroupType>("资源打包模式", ref grouptype, new EnumWindowData<GroupType>[] {
                new EnumWindowData<GroupType>(){
                    enumValue = GroupType.Folder,
                    tips = new[]{ "❀ 将文件夹下所有的文件添加至同一个Group中" },
                },
                new EnumWindowData<GroupType>(){
                    enumValue = GroupType.FileType,
                    tips = new[]{ "❀ 将文件夹下的资源按照文件类型归属之不同的Group中" },
                },
            }))
            {
                m_WindowsData.m_GroupType = (int)grouptype;
                save = true;
            }

            AssetNameType assetNameType = (AssetNameType)m_WindowsData.m_AssetNameType;
            if (EnumUI<AssetNameType>("资源打包模式", ref assetNameType, new EnumWindowData<AssetNameType>[] {
                new EnumWindowData<AssetNameType>(){
                    enumValue = AssetNameType.Assets,
                    tips = new[]{ "❀ 使用Asset路径作为 Addressable Name" },
                },
                new EnumWindowData<AssetNameType>(){
                    enumValue = AssetNameType.Root,
                    tips = new[]{ "❀ 使用资源根目录的路径作为 Addressable Name" },
                },
                new EnumWindowData<AssetNameType>(){
                    enumValue = AssetNameType.RootLocal,
                    tips = new[]{ "❀ 使用资源根目录的相对路径作为 Addressable Name" },
                },
                new EnumWindowData<AssetNameType>(){
                    enumValue = AssetNameType.FileName,
                    tips = new[]{ "❀ 使用文件名称作为 Addressable Name" },
                },
            }))
            {
                m_WindowsData.m_AssetNameType = (int)assetNameType;
                save = true;
            }


            EditorGUILayout.EndVertical();
            return save;
        }

        private bool EnumUI<T>(string title, ref T enumValue, EnumWindowData<T>[] datas) where T: System.Enum
        {
            bool save = false;
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(title);
            T grouptyep = (T)EditorGUILayout.EnumPopup(enumValue);
            if (grouptyep.Equals(enumValue) == false)
            {
                enumValue = grouptyep;
                save = true;
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            foreach (var item in datas)
            {
                if (item.enumValue.Equals(grouptyep))
                {
                    for (int i = 0; i < item.tips.Length; i++)
                    {
                        if (item.styles != null)
                        {
                            GUIStyle style = item.styles.Length > i ? item.styles[i] : null;
                            if (style == null)
                            {
                                GUILayout.Label(item.tips[i]);
                            }
                            else
                            {
                                GUILayout.Label(item.tips[i], style);
                            }
                        }
                        else
                        {
                            GUILayout.Label(item.tips[i]);
                        }
                    }
                }
            }
            return save;
        }


        private void CreateAddressableGroup()
        {

            if ((AddressableType)m_WindowsData.m_addressType == AddressableType.ResourcesFile)
            {
                List<FileInfo> allfile = new List<FileInfo>();

                foreach (var item in m_WindowsData.m_ResourceFolder)
                {
                    string folder = $"{Application.dataPath}/{item}";
                    DirectoryInfo directoryItem = new DirectoryInfo(folder);
                    FileInfo[] files = GetFile(directoryItem);

                    if ((GroupType)m_WindowsData.m_GroupType == GroupType.Folder)
                    {
                        CreateAssetGroupByDirectory(files, directoryItem, directoryItem.Name);
                    }
                    else if((GroupType)m_WindowsData.m_GroupType == GroupType.FileType)
                    {
                        var dir = FileGroup(files);

                        foreach (var fileinfo in dir)
                        {
                            CreateAssetGroupByDirectory(fileinfo.Value.ToArray(), directoryItem, $"{directoryItem.Name}_{fileinfo.Key}");
                        }
                    }
                }
            }
            else if((AddressableType)m_WindowsData.m_addressType == AddressableType.ResourcesFolder)
            {
                foreach (var item in m_WindowsData.m_ResourceFolder)
                {
                    string folder = $"{Application.dataPath}/{item}";
                    DirectoryInfo info = new DirectoryInfo(folder);

                    DirectoryInfo[] directoryInfos = info.GetDirectories();

                    foreach (var directoryItem in directoryInfos)
                    {
                        string GroupName = directoryItem.Name;
                        FileInfo[] files = GetFile(directoryItem);
                        if ((GroupType)m_WindowsData.m_GroupType == GroupType.Folder)
                        {
                            CreateAssetGroupByDirectory(files, info, GroupName);
                        }
                        else if ((GroupType)m_WindowsData.m_GroupType == GroupType.FileType)
                        {
                            var dir = FileGroup(files);

                            foreach (var fileinfo in dir)
                            {
                                CreateAssetGroupByDirectory(fileinfo.Value.ToArray(), info, $"{GroupName}_{fileinfo.Key}");
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取指定文件夹下的所有文件信息
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        private FileInfo[] GetFile(System.IO.DirectoryInfo directory)
        {
            System.IO.DirectoryInfo[] currentDirectoryInfo = directory.GetDirectories();
            System.IO.FileInfo[] currentfile = directory.GetFiles();
            List<FileInfo> allFile = new List<FileInfo>();

            foreach (var item in currentfile)
            {
                if (item.Extension == ".meta")
                    continue;
                if (item.Extension == ".cs")
                    continue;
                allFile.Add(item);
            }
            if (currentDirectoryInfo != null)
            {
                foreach (var item in currentDirectoryInfo)
                {
                    FileInfo[] fileinfo = GetFile(item);

                    allFile.AddRange(fileinfo);
                }
            }
            return allFile.ToArray();
        }

        /// <summary>
        /// 文件归类
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        private Dictionary<string, List<FileInfo>> FileGroup(FileInfo[] files)
        {
            Dictionary<string, List<FileInfo>> allResouces = new Dictionary<string, List<FileInfo>>();

            foreach (var item in files)
            {
                string key = "other";
                string extension = item.Extension.ToLower();

                switch (extension)
                {
                    case ".mp3":
                    case ".wav":
                    case ".ogg":
                    case ".aif":
                        key = "Audio";
                        break;
                    case ".avi":
                    case ".mp4":
                    case ".mov":
                    case ".ogv":
                    case ".mpg":
                    case ".mpeg":
                    case ".webm":
                        key = "Video";
                        break;
                    case ".fpx":
                    case ".obj":
                    case ".dxf":
                        key = "Model";
                        break;
                    case ".prefab":
                        key = "Gameobject";
                        break;
                    case ".anim":
                    case ".playable":
                    case ".controller":
                    case ".overridecontroller":
                        key = "Anim";
                        break;
                    case ".jpg":
                    case ".png":
                    case ".tga":
                    case ".bmp":
                        key = "Texture";
                        break;
                    case ".mat":
                        key = "Material";
                        break;
                    case ".txt":
                    case ".json":
                    case ".csv":
                        key = "Text";
                        break;
                    case ".ttf":
                    case ".otf":
                        key = "Font";
                        break;
                    case ".lighting":
                    case ".exr":
                    case ".unity":
                        key = "Scene";
                        break;
                    case ".asset":
                        if (item.Name.Contains("LightingData"))
                        {
                            key = "Scene";
                        }
                        else
                        {
                            key = "Other";
                        }
                        break;
                    default:
                        key = "Other";
                        break;
                }

                if (allResouces.ContainsKey(key))
                {
                    allResouces[key].Add(item);
                }
                else
                {
                    List<FileInfo> infos = new List<FileInfo>();
                    infos.Add(item);

                    allResouces.Add(key, infos);
                }
            }
            return allResouces;
        }

        /// <summary>
        /// 清除所有AssetGroup
        /// </summary>
        private void ClearAssetGroup()
        {
            string addressableDataPath = "Assets/AddressableAssetsData/AssetGroups";

            // 删除整个AssetGroups文件夹
            if (Directory.Exists(addressableDataPath))
            {
                FileUtil.DeleteFileOrDirectory(addressableDataPath);
                FileUtil.DeleteFileOrDirectory(addressableDataPath + ".meta");
                Debug.Log("Deleted AddressableAssetsData folder");
            }
            AssetDatabase.Refresh();
        }
        /// <summary>
        /// 创建AssetGroup
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        private AddressableAssetGroup CreateAssetGroup(string groupName)
        {
            AddressableAssetGroup group = Settings.FindGroup(groupName);
            if (group == null)
            {
                group = Settings.CreateGroup(groupName, false, false, false, new List<AddressableAssetGroupSchema>() { new BundledAssetGroupSchema(), new ContentUpdateGroupSchema() }, typeof(object));


                BundledAssetGroupSchema bundleSchema = group.GetSchema<BundledAssetGroupSchema>();
                bundleSchema.BundleNaming = BundledAssetGroupSchema.BundleNamingStyle.NoHash;
                bundleSchema.InternalIdNamingMode = BundledAssetGroupSchema.AssetNamingMode.GUID;
                bundleSchema.InternalBundleIdMode = BundledAssetGroupSchema.BundleInternalIdMode.GroupGuid;
            }
            return group;
        }
        /// <summary>
        /// 将文件信息添加至AssetGroup中
        /// </summary>
        /// <param name="fileInfos"></param>
        /// <param name="GroupName"></param>
        private void CreateAssetGroupByDirectory(FileInfo[] fileInfos,DirectoryInfo directoryInfo, string GroupName)
        {
            AddressableAssetGroup group = CreateAssetGroup(GroupName);

            foreach (var fileinfo in fileInfos)
            {
                string url = fileinfo.FullName.Replace($"\\", "/");
                string assetPath = url.Replace($"{Application.dataPath}/", "Assets/");
                // 将资源路径转换为GUID
                string guid = AssetDatabase.AssetPathToGUID(assetPath); 
                assetPath = AssetDatabase.GUIDToAssetPath(guid);
                // 检查该资源是否已被Addressables管理
                AddressableAssetEntry entry = Settings.FindAssetEntry(guid);
                if (entry == null)
                {
                    //将资源添加到目标Group
                    AddressableAssetEntry newEntry = Settings.CreateOrMoveEntry(guid, group);
                    if (newEntry != null)
                    {
                        SetEntryAddressableName(newEntry, fileinfo, directoryInfo);
                        Debug.Log($"成功将资源 '{assetPath}' 添加到Group: {group.name}");
                    }
                    else
                    {
                        Debug.LogError($"将资源 '{assetPath}' 移动到Group '{group.name}' 时失败。");
                    }
                }
                else
                {
                    AddressableAssetEntry assetEntry = group.GetAssetEntry(guid);
                    if (assetEntry == null)
                    {
                        //将资源添加到目标Group
                        AddressableAssetEntry newEntry = Settings.CreateOrMoveEntry(guid, group);
                        if (newEntry != null)
                        {
                            SetEntryAddressableName(newEntry, fileinfo, directoryInfo);
                            Debug.Log($"成功将资源 '{assetPath}' 添加到Group: {group.name}");
                        }
                        else
                        {
                            Debug.LogError($"将资源 '{assetPath}' 移动到Group '{group.name}' 时失败。");
                        }
                    }
                    else
                    {
                        SetEntryAddressableName(assetEntry, fileinfo, directoryInfo);
                        Debug.LogWarning($"{assetPath}:资源已存在");
                    }
                }
                AssetDatabase.SaveAssets();
            }
        }
        /// <summary>
        /// 设置资源的 Addressable Name
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="fileInfo"></param>
        /// <param name="directoryInfo"></param>
        private void SetEntryAddressableName(AddressableAssetEntry entry, FileInfo fileInfo,DirectoryInfo directoryInfo)
        {
            string url = fileInfo.FullName.Replace($"\\", "/");
            string dir = directoryInfo.FullName.Replace($"\\", "/");
            string assetPath = url.Replace($"{Application.dataPath}/", "Assets/");
            // 设置资源的地址（通常使用文件名或其自定义标识符）
            string label = "";
            switch ((AssetNameType)m_WindowsData.m_AssetNameType)
            {
                case AssetNameType.Assets:
                    label = assetPath;
                    break;
                case AssetNameType.FileName:
                    label = fileInfo.Name;
                    break;
                case AssetNameType.Root:
                    label = url.Replace(dir, directoryInfo.Name);
                    break;
                case AssetNameType.RootLocal:
                    label = url.Replace($"{dir}/", "");
                    break;
                default:
                    break;
            }
            entry.address = label;
        }

        /// <summary>
        /// 移除所有空的Asset Group
        /// </summary>
        public void RemoveEmptyGroups()
        {
            var emptyGroups = Settings.groups.FindAll(g => g.entries.Count == 0);

            foreach (var group in emptyGroups)
            {
                Settings.RemoveGroup(group);
            }

            AssetDatabase.Refresh();
        }
    }
}