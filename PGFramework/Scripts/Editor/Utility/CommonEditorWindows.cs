using UnityEditor;

namespace PGFrammework.Editor
{
    public class CommonEditorWindow : EditorWindow
    {

        protected static void Init<T>() where T: EditorWindow
        {
            T builder = GetWindow<T>(typeof(T).Name);
            builder.Show();
        }
        private UnityEngine.Vector2 m_Rect = new UnityEngine.Vector2(800, 600);

        private void OnGUI()
        {
            m_Rect = EditorGUILayout.BeginScrollView(m_Rect);
            EditorGUI();
            EditorGUILayout.EndScrollView();
        }

        protected virtual void EditorGUI()
        {
            
        }
    }
}
