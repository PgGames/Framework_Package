using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGFrammework.Table
{
    /// <summary>
    /// 数据表基类
    /// </summary>
    public abstract class DataTableBase
    {
        private readonly string m_Name;

        public DataTableBase() : this(null) { }
        public DataTableBase(string varName)
        {
            m_Name = varName ?? string.Empty;
        }

        /// <summary>
        /// 获取数据表行的类型。
        /// </summary>
        public abstract Type Type
        {
            get;
        }
        /// <summary>
        /// 获取数据表行数。
        /// </summary>
        public abstract int Count
        {
            get;
        }
        /// <summary>
        /// 获取数据表名称
        /// </summary>
        public string Name { get => m_Name; }
        /// <summary>
        /// 获取数据表完整名称
        /// </summary>
        public string FullName
        {
            get {
                return new TypeNamePair(Type, Name).ToString();
            }
        }
        /// <summary>
        /// 检查是否存在数据表行。
        /// </summary>
        /// <param name="id">数据表行的编号。</param>
        /// <returns>是否存在数据表行。</returns>
        public abstract bool HasDataRow(int id);
        /// <summary>
        /// 增加数据表行。
        /// </summary>
        /// <param name="dataRowString">要解析的数据表行字符串。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>是否增加数据表行成功。</returns>
        public abstract bool AddDataRow(string dataRowString, object userData);
        /// <summary>
        /// 移除指定数据表行。
        /// </summary>
        /// <param name="id">要移除数据表行的编号。</param>
        /// <returns>是否移除数据表行成功。</returns>
        public abstract bool RemoveDataRow(int id);
        /// <summary>
        /// 清空所有数据表行。
        /// </summary>
        public abstract void RemoveAllDataRows();
        /// <summary>
        /// 关闭并清理数据表。
        /// </summary>
        internal abstract void Shutdown();
    }
}
