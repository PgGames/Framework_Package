﻿using UnityEditor;
using UnityEngine;

namespace PGFrammework.PGEditor
{
    [CustomPropertyDrawer(typeof(DisplayOnly))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (Application.isPlaying)
            {
                GUI.enabled = false;
                EditorGUI.PropertyField(position, property, label, false);
            }
            else
            {
                GUI.enabled = true;
                EditorGUI.PropertyField(position, property, label, true);
            }
        }
    }
}