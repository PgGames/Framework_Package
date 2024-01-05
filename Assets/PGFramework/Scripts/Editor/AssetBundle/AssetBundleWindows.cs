using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;


namespace PGFrammework.PGEditor
{
    public class AssetBundleWindows : EditorWindow
    {
        internal static AssetBundleDate data;

        [MenuItem(CommonWindow.Tools + "/Assetbundle", false, 1003)]
        static void Windows()
        {
            AssetBundleWindows assetBundleBuilder = EditorWindow.GetWindow(typeof(AssetBundleWindows)) as AssetBundleWindows;
            assetBundleBuilder.titleContent = new GUIContent(typeof(AssetBundleWindows).Name);
            assetBundleBuilder.Show();
        }
        private void OnEnable()
        {
            data = CommonWindow.ReadDate<AssetBundleDate>();
        }
        private void OnDisable()
        {
            CommonWindow.SaveDate<AssetBundleDate>(data);
        }
        protected Vector2 mRect { set; get; }
        private void OnGUI()
        {
            mRect = EditorGUILayout.BeginScrollView(mRect);
            UI_GUI();
            EditorGUILayout.EndScrollView();
        }
        protected void UI_GUI()
        {
            EditorGUILayout.Space();
            CommonEditorUI.CenterLabel("AssetBundle");
            data.mAssetFoler = CommonEditorUI.GUI_SelectDirectory(data.mAssetFoler, "AssetFoler:");
            data.mAssetBundleFoler = CommonEditorUI.GUI_SelectDirectory(data.mAssetBundleFoler, "AssetBundleFoler:");
            EditorGUILayout.Space();
            data.mIsDelectManifest = GUILayout.Toggle(data.mIsDelectManifest, "Delect Manifest");
            EditorGUILayout.Space();
            if (CommonEditorUI.CenterButton("Build AssetBundle"))
            {
                Click_ClearAssetBundleNames();
                Click_SettingAssetBundle();
                Click_BuildAssetBundle();

                Delect_Manifest_File();
            }
        }
        /// <summary>
        /// 清除所有的AssetBundle名称
        /// </summary>
        private void Click_ClearAssetBundleNames()
        {
            string[] assetbundleNames = AssetDatabase.GetAllAssetBundleNames();
            if (assetbundleNames != null)
            {
                for (int i = 0; i < assetbundleNames.Length; i++)
                {
                    AssetDatabase.RemoveAssetBundleName(assetbundleNames[i], true);
                }
            }
        }
        /// <summary>
        /// 设置AssetBundle的名称
        /// </summary>
        private void Click_SettingAssetBundle()
        {
            FileInfo[] tempFileInfo = CommonWindow.ReadFileExtension(new DirectoryInfo(data.mAssetFoler), ".meta");

            CommonWindow.DisplayProgressBar("AssetBundle", "", 0);
            for (int i = 0; i < tempFileInfo.Length; i++)
            {
                FileInfo TempFile = tempFileInfo[i];

                string tempDirectoryName = TempFile.DirectoryName.Replace("\\", "/");
                tempDirectoryName = tempDirectoryName.Replace(Application.dataPath, "");
                tempDirectoryName = tempDirectoryName.Trim('/');
                string assetbundleName = tempDirectoryName;
                string dir_path = TempFile.FullName.Replace("\\", "/");
                string asset_path = dir_path.Replace(Application.dataPath, "Assets");

                AssetImporter asset = AssetImporter.GetAtPath(asset_path);
                if (asset != null)
                {
                    asset.assetBundleName = assetbundleName;
                    asset.assetBundleVariant = "bundle";
                }
                CommonWindow.DisplayProgressBar("AssetBundle", TempFile.FullName, (i * 1.0f) / tempFileInfo.Length);
            }
            CommonWindow.DisplayProgressBar("AssetBundle", "", 1);
            //清除无效的assetbundle标记
            AssetDatabase.RemoveUnusedAssetBundleNames();
            AssetDatabase.Refresh();
        }
        /// <summary>
        /// 导出AssetBundle资源
        /// </summary>
        private void Click_BuildAssetBundle()
        {
            string tempOutFoler = data.mAssetBundleFoler;

            BuildTarget target = BuildTarget.NoTarget;
#if UNITY_STANDALONE_WIN
            tempOutFoler = string.Format("{0}/Windows", data.mAssetBundleFoler);
            target = BuildTarget.StandaloneWindows;
#elif UNITY_ANDROID
		tempOutFoler = string.Format("{0}/Android",data.mAssetBundleFoler);
		target = BuildTarget.Android;
#elif UNITY_IOS || UNITY_IPHONE
		tempOutFoler = string.Format("{0}/IOS",data.mAssetBundleFoler);
		target = BuildTarget.iOS;
#endif
            if (!Directory.Exists(tempOutFoler))
            {
                Directory.CreateDirectory(tempOutFoler);
            }
            BuildPipeline.BuildAssetBundles(tempOutFoler, BuildAssetBundleOptions.None, target);
            AssetDatabase.Refresh();
        }
        /// <summary>
        /// 删除 Manifest 文件
        /// </summary>
        private void Delect_Manifest_File()
        {
            if (!data.mIsDelectManifest)
                return;
            string tempOutFoler = data.mAssetBundleFoler;
            string tempFileName = "";
#if UNITY_STANDALONE_WIN
            tempFileName = "Windows";
#elif UNITY_ANDROID
        tempFileName = "Android";
#elif UNITY_IOS || UNITY_IPHONE
        tempFileName = "IOS";
#endif
            tempOutFoler = string.Format("{0}/{1}", data.mAssetBundleFoler, tempFileName);
            FileInfo[] files = CommonWindow.ReadFile(new DirectoryInfo(tempOutFoler), ".manifest");
            CommonWindow.DisplayProgressBar("Delect Manifest", "", 0);
            for (int i = 0; i < files.Length; i++)
            {
                string filename = files[i].FullName;
                //忽略总 Manifest 文件
                if (files[i].Name == tempFileName)
                    continue;
                //删除无效的 Manifest 文件
                File.Delete(files[i].FullName);
                CommonWindow.DisplayProgressBar("Delect Manifest", filename, (i * 1.0f) / files.Length);
            }
            CommonWindow.DisplayProgressBar("Delect Manifest", "", 1);
            AssetDatabase.Refresh();
        }
    }

    [System.Serializable]
    public class AssetBundleDate : BaseDate
    {
        public string mAssetFoler;
        public string mAssetBundleFoler;
        public bool mIsDelectManifest;

        public override void Init()
        {
            mIsDelectManifest = false;
            mAssetFoler = Application.dataPath + "/HotResources";
            mAssetBundleFoler = Application.streamingAssetsPath;
        }
    }
}
