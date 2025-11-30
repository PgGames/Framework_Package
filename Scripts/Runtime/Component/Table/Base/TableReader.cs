using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace PGFrammework.Table
{
    public static class TableReader
    {
        static readonly Dictionary<Type, DataTableBase> m_DataTable = new Dictionary<Type, DataTableBase>();

        /// <summary>
        /// 检查数据表是否已存在
        /// </summary>
        /// <param name="tableType"></param>
        /// <returns></returns>
        public static bool HasDataTable(Type tableType)
        {
            if (!m_DataTable.ContainsKey(tableType))
                return false;
            return true;
        }

        /// <summary>
        /// 创建表格数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static IDataTable<T> CreateDataTable<T>(string path) where T : class, IDataRow, new()
        {
            string fileName = Path.GetFileNameWithoutExtension(path);

            DataTable<T> data = new DataTable<T>(fileName);

            ReadData(path, typeof(T), data);

            return data;
        }
        /// <summary>
        /// 创建表格数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static IDataTable<T> CreateDataTable<T>(TextAsset varTextAsset) where T : class, IDataRow, new()
        {
            string tempDataTableString = varTextAsset.text;

            DataTable<T> data = new DataTable<T>(varTextAsset.name);


            ReadData(varTextAsset, typeof(T), data);
            return data;
        }


        /// <summary>
        /// 加载表格
        /// </summary>
        /// <param name="path">表格路径</param>
        /// <param name="varTableName">表格名称</param>
        /// <param name="success">加载回调</param>
        /// <param name="userData">用户自定义数据</param>
        public static void LoadDataTable(string path, string varTableName, LoadDataTableCall success, object userData)
        {
            GetDataTable(varTableName, out Type dataRowType, out DataTableBase dataTable);

            try
            {
                if (HasDataTable(dataRowType) == false)
                {
                    ReadData(path, dataRowType, dataTable);
                    success?.Invoke(true, "", userData);
                }
                else
                {
                    success?.Invoke(false, $"{varTableName} Table is exist", userData);
                }
            }
            catch (Exception exp)
            {
                success?.Invoke(false, $"{varTableName} Table loading error {exp.Message}", userData);
            }
        }
        /// <summary>
        /// 加载表格
        /// </summary>
        /// <param name="varTextAsset">表格数据</param>
        /// <param name="varTableName">表格名称</param>
        /// <param name="success">加载回调</param>
        /// <param name="userData">用户自定义数据</param>
        public static void LoadDataTable(TextAsset varTextAsset, string varTableName, LoadDataTableCall success, object userData)
        {
            GetDataTable(varTableName, out Type dataRowType, out DataTableBase dataTable);
            try
            {
                if (HasDataTable(dataRowType))
                {
                    ReadData(varTextAsset, dataRowType, dataTable);
                    success?.Invoke(true, "", userData);
                }
                else
                {
                    success?.Invoke(false, $"{varTableName} Table is exist", userData);
                }
            }
            catch (Exception exp)
            {
                success?.Invoke(false, $"{varTableName} Table loading error {exp.Message}", userData);
            }

        }


        /// <summary>
        /// 获取表格数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IDataTable<T> GetDataTable<T>() where T : class, IDataRow, new()
        {
            Type key = typeof(T);

            if (!m_DataTable.ContainsKey(key))
                return null;
            DataTableBase data = m_DataTable[key];

            return data as DataTable<T>;
        }

        /// <summary>
        /// 注销表格
        /// </summary>
        /// <param name="varDataTableName"></param>
        public static void UnDataTable(string varDataTableName)
        {
            string dataRowClassName = string.Format("{0}{1}", DataTableExtension.DataRowClassPrefixName, varDataTableName);

            Type dataRowType = Type.GetType(dataRowClassName);

            UnDataTable(dataRowType);
        }
        /// <summary>
        /// 注销表格
        /// </summary>
        /// <param name="dataRowType"></param>
        public static void UnDataTable(Type dataRowType)
        {
            if (m_DataTable.ContainsKey(dataRowType))
            {
                m_DataTable.Remove(dataRowType);
            }
        }

        /// <summary>
        /// 获取表格类型
        /// </summary>
        /// <param name="varTableName"></param>
        /// <param name="dataRowType"></param>
        /// <param name="dataTable"></param>
        private static void GetDataTable(string varTableName, out Type dataRowType, out DataTableBase dataTable)
        {
            string dataRowClassName = string.Format("{0}{1}", DataTableExtension.DataRowClassPrefixName, varTableName);

            dataRowType = Type.GetType(dataRowClassName);
            if (dataRowType == null)
            {
                throw new Exception($"Can not get data row type with class name '{dataRowClassName}'.");
            }
            if (!typeof(IDataRow).IsAssignableFrom(dataRowType))
            {
                throw new Exception(string.Format("Data row type '{0}' is invalid.", dataRowType.FullName));
            }

            Type dataTableType = typeof(DataTable<>).MakeGenericType(dataRowType);
            dataTable = (DataTableBase)Activator.CreateInstance(dataTableType, varTableName);
        }


        /// <summary>
        /// 创建表格
        /// </summary>
        /// <param name="path"></param>
        /// <param name="dataRowType"></param>
        /// <param name="dataTable"></param>
        private static DataTableBase ReadData(string path, Type dataRowType, DataTableBase dataTable)
        {
            string fileName = Path.GetFileNameWithoutExtension(path);
            string tempDataTableString = File.ReadAllText(path);
            return ReadDataContent(tempDataTableString, dataRowType, dataTable);
        }
        /// <summary>
        /// 创建表格
        /// </summary>
        /// <param name="varTextAsset"></param>
        /// <param name="dataRowType"></param>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        private static DataTableBase ReadData(TextAsset varTextAsset, Type dataRowType, DataTableBase dataTable)
        {
            string tempDataTableString = varTextAsset.text;

            return ReadDataContent(tempDataTableString, dataRowType, dataTable);
        }
        private static DataTableBase ReadDataContent(string varTable, Type dataRowType, DataTableBase dataTable)
        {
            string[] TempDataTable = varTable.Split(DataTableExtension.DataLineSplitSeparators, StringSplitOptions.None);


            for (int i = 4; i < TempDataTable.Length; i++)
            {
                string content = TempDataTable[i];
                if (string.IsNullOrEmpty(content) == false)
                {
                    int index = content.IndexOfAny(DataTableExtension.DataSplitSeparators);
                    string str = content.Substring(0, index);
                    //忽略注释行
                    if (str.Contains("//") || str.Contains(DataTableExtension.CommentLineSeparator))
                    {
                        continue;
                    }
                    dataTable.AddDataRow(str, null);
                }
            }
            m_DataTable.Add(dataRowType, dataTable);
            return dataTable;
        }
    }
}
