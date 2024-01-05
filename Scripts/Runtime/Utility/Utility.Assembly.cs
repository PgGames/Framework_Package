using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PGFrammework
{
    public static partial class Utility
    {
        public static class Assembly
        {
            private static readonly System.Reflection.Assembly[] s_Assemblies = null;
            private static readonly Dictionary<string, Type> s_CachedTypes = new Dictionary<string, Type>(StringComparer.Ordinal);

            static Assembly()
            {
                s_Assemblies = AppDomain.CurrentDomain.GetAssemblies();
            }


            /// <summary>
            /// 获取已加载的程序集中的指定类型。
            /// </summary>
            /// <param name="typeName">要获取的类型名。</param>
            /// <returns>已加载的程序集中的指定类型。</returns>
            public static Type GetType(string typeName)
            {
                if (string.IsNullOrEmpty(typeName))
                {
                    throw new Exception("Type name is invalid.");
                }

                Type type = null;
                if (s_CachedTypes.TryGetValue(typeName, out type))
                {
                    return type;
                }

                type = Type.GetType(typeName);
                if (type != null)
                {
                    s_CachedTypes.Add(typeName, type);
                    return type;
                }

                foreach (System.Reflection.Assembly assembly in s_Assemblies)
                {
                    type = Type.GetType(Utility.Text.Format("{0}, {1}", typeName, assembly.FullName));
                    if (type != null)
                    {
                        s_CachedTypes.Add(typeName, type);
                        return type;
                    }
                }

                return null;
            }
            /// <summary>
            /// 获取已加载的程序集中的所有类型。
            /// </summary>
            /// <returns>已加载的程序集中的所有类型</returns>
            public static Type[] GetTypes()
            {
                List<Type> tempList = new List<Type>();
                foreach (var item in s_Assemblies)
                {
                    Type[] types = item.GetTypes();
                    tempList.AddRange(types);
                }
                return tempList.ToArray();
            }

            /// <summary>
            /// 获取继承了指定基类的程序类
            /// </summary>
            /// <returns></returns>
            public static Type[] GetClassByType(Type baseType)
            {
                List<Type> tempTypes = new List<Type>();

                foreach (var item in s_Assemblies)
                {
                    System.Reflection.Assembly assembly = item;

                    if (assembly == null)
                        continue;

                    Type[] arrayTypes = GetClassByAssembly(assembly, baseType);
                    tempTypes.AddRange(arrayTypes);
                }
                return tempTypes.ToArray();
            }
            private static Type[] GetClassByAssembly(System.Reflection.Assembly assembly, Type baseType)
            {
                List<Type> tempList = new List<Type>();

                Type[] tyeps = assembly.GetTypes();
                foreach (var item in tyeps)
                {
                    if (item.IsClass && !item.IsAbstract)
                    {
                        if (baseType.IsAssignableFrom(item))
                        {
                            tempList.Add(item);
                        }
                    }
                }
                return tempList.ToArray();
            }
        }
    }
}