using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PGFrammework
{
    public class GameComponent
    {
        private Dictionary<string, FrameworkComponent> Components = new Dictionary<string, FrameworkComponent>();

        private static GameComponent component;

        public static GameComponent Instance
        {
            get
            {
                if (component == null)
                {
                    component = new GameComponent();
                    //component.Init();
                }
                return component;
            }
        }
        /// <summary>
        /// 框架初始化
        /// </summary>
        public void Init()
        {
            Type[] frameworkComponent = Utility.Assembly.GetClassByType(typeof(FrameworkComponent));

            List<UnityEngine.Object> tempObject = new List<UnityEngine.Object>();

            for (int i = 0; i < frameworkComponent.Length; i++)
            {
                Type frameworkName = frameworkComponent[i];

                if (!typeof(FrameworkComponent).IsAssignableFrom(frameworkName))
                    continue;
                UnityEngine.Object TempObject = GameObject.FindObjectOfType(frameworkName);
                if (TempObject == null)
                {
                    GameObject tempGame = new GameObject(frameworkName.Name);
                    GameObject.DontDestroyOnLoad(tempGame);
                    TempObject = tempGame.AddComponent(frameworkName);
                }
                tempObject.Add(TempObject);
            }
            for (int i = 0; i < tempObject.Count; i++)
            {
                FrameworkComponent component = tempObject[i] as FrameworkComponent;
                if (component != null)
                {
                    component.Init();
                }
            }
        }
        public void RegComponent<T>(T component) where T : FrameworkComponent
        {
            string key = component.GetType().Name;
            if (Components.ContainsKey(key))
            {
                return;
            }
            component.Init();
            Components.Add(key, component);
        }
        public T GetComponent<T>() where T : FrameworkComponent
        {
            if (Components.ContainsKey(typeof(T).Name))
            {
                return Components[typeof(T).Name] as T;
            }
            return null;
        }
    }
}
