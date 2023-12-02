using PGFrammework.Runtime;
using UnityEditor;

namespace PGFrammework.Editor
{
    [CustomEditor(typeof(EventComponent))]
    public class EventComponentInspector : FrameworkInspector
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.HelpBox("Available during runtime only.", MessageType.Info);

        }
    }
}