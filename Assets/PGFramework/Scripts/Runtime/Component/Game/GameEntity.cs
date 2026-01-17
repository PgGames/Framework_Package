using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGFrammework.Runtime
{
    public class GameEntity
    {
        private static IEventComponent m_Event;
        private static IUIComponent m_UI;

        private static IResourcesComponent m_Resourse;


        /// <summary>
        /// 事件模块
        /// </summary>
        public static IEventComponent Event
        {
            get {
                if (m_Event == null)
                {
                    m_Event = GameComponent.Instance.GetComponent<EventComponent>();
                }
                return m_Event;
            }
        }

        public static IUIComponent UI
        {
            get {
                if (m_UI == null)
                {
                    m_UI = GameComponent.Instance.GetComponent<UIComponent>();
                }
                return m_UI;
            }
        }

        public static IResourcesComponent Resources
        {
            get {
                if (m_Resourse == null)
                {
                    m_Resourse = GameComponent.Instance.GetComponent<ResourcesComponent>();
                }
                return m_Resourse;
            }
        }

        
    }
}
