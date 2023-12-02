using PGFrammework.Runtime;
using UnityEditor;
using UnityEngine;

namespace PGFrammework.Editor
{
    [CustomEditor(typeof(BaseComponent))]
    public class BaseComponentInspector :FrameworkInspector
    {

        private readonly string[] m_GameSpeedNameArray = new string[] { "0x", "0.125x", "0.25x", "0.5x", "1x", "2x", "4x", "8x" };
        private readonly float[] m_GameSpeedValueArray = new float[] { 0, 0.125f, 0.25f, 0.5f, 1, 2, 4, 8 };

        private SerializedProperty m_EditorVersionHelperTypeName = null;
        private SerializedProperty m_EditorDebugHelperTypeName = null;
        private SerializedProperty m_EditorJsonHelperTypeName = null;
        private SerializedProperty m_EditorFrameRate = null;
        private SerializedProperty m_EditorGameSpeed = null;
        private SerializedProperty m_EditormRunInBackground = null;
        private SerializedProperty m_EditorNeverSleep = null;


        private string[] m_VersionHelperTypeNames = null;
        private int m_VersionHelperTypeNameIndex = 0;
        private string[] m_DebugHelperTypeNames = null;
        private int m_DebugHelperTypeNameIndex = 0;
        private string[] m_JsonHelperTypeNames = null;
        private int m_JsonHelperTypeNameIndex = 0;


        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            serializedObject.Update();
            
            BaseComponent t = (BaseComponent)target;


            EditorGUI.BeginDisabledGroup(EditorApplication.isPlayingOrWillChangePlaymode);
            {

                EditorGUILayout.BeginVertical("box");
                {
                    EditorGUILayout.LabelField("Global Helpers", EditorStyles.boldLabel);

                    CommonEditorUI.GUI_Helper("Version Helper", m_EditorVersionHelperTypeName.stringValue, m_VersionHelperTypeNames, ref m_VersionHelperTypeNameIndex);

                    CommonEditorUI.GUI_Helper("Debug Helper", m_EditorDebugHelperTypeName.stringValue, m_DebugHelperTypeNames, ref m_DebugHelperTypeNameIndex);

                    CommonEditorUI.GUI_Helper("JSON Helper", m_EditorJsonHelperTypeName.stringValue, m_JsonHelperTypeNames, ref m_JsonHelperTypeNameIndex);
                }
                EditorGUILayout.EndVertical();

                int frameRate = EditorGUILayout.IntSlider("Frame Rate", m_EditorFrameRate.intValue, 1, 120);
                if (frameRate != m_EditorFrameRate.intValue)
                {
                    if (Application.isPlaying)
                    {
                        t.FrameRate = frameRate;
                    }
                    else
                    {
                        m_EditorFrameRate.intValue = frameRate;
                    }
                }

                EditorGUILayout.BeginVertical("box");
                {
                    float tempvalue = EditorGUILayout.Slider("Game Speed", m_EditorGameSpeed.floatValue, 0, 8);

                    int selecyid = GUILayout.SelectionGrid(-1, m_GameSpeedNameArray, 4);

                    if (selecyid >= 0 && selecyid < m_GameSpeedValueArray.Length)
                    {
                        tempvalue = m_GameSpeedValueArray[selecyid];
                    }

                    if (tempvalue != m_EditorGameSpeed.floatValue)
                    {
                        m_EditorGameSpeed.floatValue = tempvalue;
                    }
                }
                EditorGUILayout.EndVertical();



                m_EditormRunInBackground.boolValue = EditorGUILayout.BeginToggleGroup("Run In Background", m_EditormRunInBackground.boolValue);
                EditorGUILayout.EndToggleGroup();

                m_EditorNeverSleep.boolValue = EditorGUILayout.BeginToggleGroup("Never Sleep", m_EditorNeverSleep.boolValue);
                EditorGUILayout.EndToggleGroup();

            }

            EditorGUI.EndDisabledGroup();

            serializedObject.ApplyModifiedProperties();
        }

        private void OnEnable()
        {
            m_EditorVersionHelperTypeName = serializedObject.FindProperty("m_VersionHelperTypeName");
            m_EditorDebugHelperTypeName = serializedObject.FindProperty("m_DebugHelperTypeName");
            m_EditorJsonHelperTypeName = serializedObject.FindProperty("m_JsonHelperTypeName");
            m_EditorFrameRate = serializedObject.FindProperty("m_FrameRate");
            m_EditorGameSpeed = serializedObject.FindProperty("m_GameSpeed");
            m_EditormRunInBackground = serializedObject.FindProperty("m_RunInBackground");
            m_EditorNeverSleep = serializedObject.FindProperty("m_NeverSleep");


            RefreshTypeNames();
        }



        private void RefreshTypeNames()
        {
            CommonEditorUI.GetHelperList<Version.IVersionHelper>(m_EditorVersionHelperTypeName.stringValue, out m_VersionHelperTypeNames, out m_VersionHelperTypeNameIndex);
            CommonEditorUI.GetHelperList<FrameworkLog.ILogHelper>(m_EditorDebugHelperTypeName.stringValue, out m_DebugHelperTypeNames, out m_DebugHelperTypeNameIndex);
            CommonEditorUI.GetHelperList<FrameworkJson.IJsonHelper>(m_EditorJsonHelperTypeName.stringValue, out m_JsonHelperTypeNames, out m_JsonHelperTypeNameIndex);
        }
    }
}