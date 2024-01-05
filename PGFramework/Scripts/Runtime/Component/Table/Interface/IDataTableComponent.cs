using PGFrammework.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PGFrammework
{
    public interface IDataTableComponent
    {
        /// <summary>
        /// 检查数据表是否已存在
        /// </summary>
        /// <param name="tableType"></param>
        /// <returns></returns>
        public bool HasDataTable(Type tableType);

        /// <summary>
        /// 加载表格
        /// </summary>
        /// <param name="path">表格路径</param>
        /// <param name="varTableName">表格名称</param>
        /// <param name="success">加载回调</param>
        /// <param name="userData">用户自定义数据</param>
        public void LoadDataTable(string path, string varTableName, LoadDataTableCall success, object userData);
        /// <summary>
        /// 加载表格
        /// </summary>
        /// <param name="varTextAsset">表格数据</param>
        /// <param name="varTableName">表格名称</param>
        /// <param name="success">加载回调</param>
        /// <param name="userData">用户自定义数据</param>
        public void LoadDataTable(TextAsset varTextAsset, string varTableName, LoadDataTableCall success, object userData);
        /// <summary>
        /// 创建表格数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public IDataTable<T> CreateDataTable<T>(string path) where T : class, IDataRow, new();
        /// <summary>
        /// 创建表格数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public IDataTable<T> CreateDataTable<T>(TextAsset varTextAsset) where T : class, IDataRow, new();
        /// <summary>
        /// 获取表格数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IDataTable<T> GetDataTable<T>() where T : class, IDataRow, new();

        /// <summary>
        /// 注销表格
        /// </summary>
        /// <param name="varDataTableName"></param>
        public void UnDataTable(string varDataTableName);
        /// <summary>
        /// 注销表格
        /// </summary>
        /// <param name="dataRowType"></param>
        public void UnDataTable(Type dataRowType);
    }
}
