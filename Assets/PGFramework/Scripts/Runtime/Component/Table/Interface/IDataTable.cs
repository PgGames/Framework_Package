using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGFrammework.Table
{
    public interface IDataTable<T> : IEnumerable<T> where T : IDataRow
    {
        /// <summary>
        /// 获取数据表名称
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 获取数据表完整的名称
        /// </summary>
        string FullName { get; }
        /// <summary>
        /// 获取数据表行的类型
        /// </summary>
        Type Type { get; }
        /// <summary>
        /// 获取数据表行的数量
        /// </summary>
        int Count { get; }
        /// <summary>
        /// 获取数据表指定行的数据
        /// </summary>
        /// <param name="id">数据表行的编号</param>
        /// <returns>数据表行的数据</returns>
        T this[int id] { get; }
        /// <summary>
        /// 检查指定数据是否存在
        /// </summary>
        /// <param name="id">数据表行的编号</param>
        /// <returns>是否存在数据表行。</returns>
        bool HasDataRow(int id);
        /// <summary>
        /// 检查是否存在数据表行。
        /// </summary>
        /// <param name="condition">要检查的条件。</param>
        /// <returns>是否存在数据表行。</returns>
        bool HasDataRow(Predicate<T> condition);

        /// <summary>
        /// 获取数据表行。
        /// </summary>
        /// <param name="id">数据表行的编号。</param>
        /// <returns>数据表行。</returns>
        T GetDataRow(int id);

        /// <summary>
        /// 获取符合条件的数据表行。
        /// </summary>
        /// <param name="condition">要检查的条件。</param>
        /// <returns>符合条件的数据表行。</returns>
        /// <remarks>当存在多个符合条件的数据表行时，仅返回第一个符合条件的数据表行。</remarks>
        T GetDataRow(Predicate<T> condition);

        /// <summary>
        /// 获取符合条件的数据表行。
        /// </summary>
        /// <param name="condition">要检查的条件。</param>
        /// <returns>符合条件的数据表行。</returns>
        T[] GetDataRows(Predicate<T> condition);

        /// <summary>
        /// 获取符合条件的数据表行。
        /// </summary>
        /// <param name="condition">要检查的条件。</param>
        /// <param name="results">符合条件的数据表行。</param>
        void GetDataRows(Predicate<T> condition, List<T> results);

        /// <summary>
        /// 获取排序后的数据表行。
        /// </summary>
        /// <param name="comparison">要排序的条件。</param>
        /// <returns>排序后的数据表行。</returns>
        T[] GetDataRows(Comparison<T> comparison);

        /// <summary>
        /// 获取排序后的数据表行。
        /// </summary>
        /// <param name="comparison">要排序的条件。</param>
        /// <param name="results">排序后的数据表行。</param>
        void GetDataRows(Comparison<T> comparison, List<T> results);

        /// <summary>
        /// 获取排序后的符合条件的数据表行。
        /// </summary>
        /// <param name="condition">要检查的条件。</param>
        /// <param name="comparison">要排序的条件。</param>
        /// <returns>排序后的符合条件的数据表行。</returns>
        T[] GetDataRows(Predicate<T> condition, Comparison<T> comparison);

        /// <summary>
        /// 获取排序后的符合条件的数据表行。
        /// </summary>
        /// <param name="condition">要检查的条件。</param>
        /// <param name="comparison">要排序的条件。</param>
        void GetDataRows(Predicate<T> condition, Comparison<T> comparison, List<T> results);

        /// <summary>
        /// 获取所有数据表行。
        /// </summary>
        /// <returns>所有数据表行。</returns>
        T[] GetAllDataRows();

        /// <summary>
        /// 获取所有数据表行。
        /// </summary>
        /// <param name="results">所有数据表行。</param>
        void GetAllDataRows(List<T> results);
        /// <summary>
        /// 增加数据表行。
        /// </summary>
        /// <param name="dataRowString">要解析的数据表行字符串。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>是否增加数据表行成功。</returns>
        bool AddDataRow(string dataRowString, object userData);
        /// <summary>
        /// 移除指定数据表行。
        /// </summary>
        /// <param name="id">要移除数据表行的编号。</param>
        /// <returns>是否移除数据表行成功。</returns>
        bool RemoveDataRow(int id);

        /// <summary>
        /// 清空所有数据表行。
        /// </summary>
        void RemoveAllDataRows();
    }
}
