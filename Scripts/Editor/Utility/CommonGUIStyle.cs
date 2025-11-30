using UnityEditor;
using UnityEngine;

namespace PGFrammework.PGEditor
{
    public class CommonGUIStyle
    {
        private static GUIStyle m_TitleStyle;

        public static GUIStyle TitleStyle
        {
            get
            {
                if (m_TitleStyle == null)
                {
                    m_TitleStyle = new GUIStyle()
                    {
                        fontSize = 15,

                        normal = new GUIStyleState() 
                        {
                            textColor = Color.white
                        }
                    };
                }
                return m_TitleStyle;
            }
        }

        private static GUIStyle m_WarningStyle;

        public static GUIStyle WarningStyle
        {
            get
            {
                if (m_WarningStyle == null)
                {
                    m_WarningStyle = new GUIStyle()
                    {
                        fontSize = 14,

                        normal = new GUIStyleState()
                        {
                            textColor = Color.yellow
                        }
                    };
                }
                return m_WarningStyle;
            }
        }
        private static GUIStyle m_ErrorStyle;
        public static GUIStyle ErrorStyle
        {
            get
            {
                if (m_ErrorStyle == null)
                {
                    m_ErrorStyle = new GUIStyle()
                    {
                        fontSize = 14,

                        normal = new GUIStyleState()
                        {
                            textColor = Color.red
                        }
                    };
                }
                return m_ErrorStyle;
            }
        }
    }
}