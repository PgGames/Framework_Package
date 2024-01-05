using PGFrammework.Runtime;
using UnityEditor;

namespace PGFrammework.PGEditor
{

    [CustomEditor(typeof(DebugComponent))]
    public class DebugComponentInspector : FrameworkInspector
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }
}