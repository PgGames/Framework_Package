using PGFrammework.Table;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PGFrammework
{
    public class DataTableComponent : FrameworkComponent
    {
        public override void Init()
        {
        }
        /// <summary>
        /// ������ݱ��Ƿ��Ѵ���
        /// </summary>
        /// <param name="tableType"></param>
        /// <returns></returns>
        public bool HasDataTable(Type tableType)
        {
            return TableReader.HasDataTable(tableType);
        }

        /// <summary>
        /// ���ر��
        /// </summary>
        /// <param name="path">���·��</param>
        /// <param name="varTableName">�������</param>
        /// <param name="success">���ػص�</param>
        /// <param name="userData">�û��Զ�������</param>
        public void LoadDataTable(string path, string varTableName, LoadDataTableCall success, object userData)
        {
            TableReader.LoadDataTable(path, varTableName, success, userData);
        }
        /// <summary>
        /// ���ر��
        /// </summary>
        /// <param name="varTextAsset">�������</param>
        /// <param name="varTableName">�������</param>
        /// <param name="success">���ػص�</param>
        /// <param name="userData">�û��Զ�������</param>
        public void LoadDataTable(TextAsset varTextAsset, string varTableName, LoadDataTableCall success, object userData)
        {
            TableReader.LoadDataTable(varTextAsset, varTableName, success, userData);
        }
        /// <summary>
        /// �����������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public IDataTable<T> CreateDataTable<T>(string path) where T : class, IDataRow, new()
        {
            return TableReader.CreateDataTable<T>(path);
        }
        /// <summary>
        /// �����������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public IDataTable<T> CreateDataTable<T>(TextAsset varTextAsset) where T : class, IDataRow, new()
        {
            return TableReader.CreateDataTable<T>(varTextAsset);
        }
        /// <summary>
        /// ��ȡ�������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IDataTable<T> GetDataTable<T>() where T : class, IDataRow, new()
        {
            return TableReader.GetDataTable<T>();
        }

        /// <summary>
        /// ע�����
        /// </summary>
        /// <param name="varDataTableName"></param>
        public void UnDataTable(string varDataTableName)
        {
            TableReader.UnDataTable(varDataTableName);
        }
        /// <summary>
        /// ע�����
        /// </summary>
        /// <param name="dataRowType"></param>
        public void UnDataTable(Type dataRowType)
        {
            TableReader.UnDataTable(dataRowType);
        }

    }
}