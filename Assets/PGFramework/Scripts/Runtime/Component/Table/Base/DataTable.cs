using PGFrammework.Table;
using System;
using System.Collections;
using System.Collections.Generic;


namespace PGFrammework.Table
{
    public class DataTable<T> : DataTableBase, IDataTable<T> where T : class, IDataRow, new()
    {
        private readonly Dictionary<int, T> m_DataSet;

        public DataTable(string name) : base(name)
        {
            m_DataSet = new Dictionary<int, T>();
        }


        public override Type Type { get { return typeof(T); } }
        public override int Count { get { return m_DataSet.Count; } }
        public T this[int id] { get { return GetDataRow(id); } }


        public override bool HasDataRow(int id)
        {
            return m_DataSet.ContainsKey(id);
        }

        public bool HasDataRow(Predicate<T> condition)
        {
            if (condition == null)
            {
                throw new Exception("Condition is invalid.");
            }

            foreach (KeyValuePair<int, T> dataRow in m_DataSet)
            {
                if (condition(dataRow.Value))
                {
                    return true;
                }
            }

            return false;
        }
        public override bool AddDataRow(string dataRowString, object userData)
        {
            try
            {
                T dataRow = new T();
                if (!dataRow.ParseDataRow(dataRowString, userData))
                {
                    return false;
                }

                if (m_DataSet.ContainsKey(dataRow.Id))
                {
                    throw new Exception(string.Format("Already exist '{0}' in data table '{1}'.", dataRow.Id, new TypeNamePair(typeof(T), Name)));
                }
                m_DataSet.Add(dataRow.Id, dataRow);
                return true;
            }
            catch (Exception exception)
            {
                if (exception is Exception)
                {
                    throw;
                }

                throw new Exception(string.Format("Can not parse data row string for data table '{0}' with exception '{1}'.", new TypeNamePair(typeof(T), Name), exception), exception);
            }
        }



        public T GetDataRow(int id)
        {
            if (m_DataSet.TryGetValue(id, out T daterow))
            {
                return daterow;
            }
            return null;
        }

        /// <summary>
        /// 获取符合条件的数据表行。
        /// </summary>
        /// <param name="condition">要检查的条件。</param>
        /// <returns>符合条件的数据表行。</returns>
        /// <remarks>当存在多个符合条件的数据表行时，仅返回第一个符合条件的数据表行。</remarks>
        public T GetDataRow(Predicate<T> condition)
        {
            if (condition == null)
            {
                throw new Exception("Condition is invalid.");
            }

            foreach (KeyValuePair<int, T> dataRow in m_DataSet)
            {
                if (condition(dataRow.Value))
                {
                    return dataRow.Value;
                }
            }
            return null;
        }

        public T[] GetAllDataRows()
        {
            int index = 0;
            T[] results = new T[m_DataSet.Count];
            foreach (KeyValuePair<int, T> dataRow in m_DataSet)
            {
                results[index++] = dataRow.Value;
            }

            return results;
        }

        public void GetAllDataRows(List<T> results)
        {
            if (results == null)
            {
                throw new Exception("Results is invalid.");
            }

            results.Clear();
            foreach (KeyValuePair<int, T> dataRow in m_DataSet)
            {
                results.Add(dataRow.Value);
            }
        }
        public T[] GetDataRows(Predicate<T> condition)
        {
            if (condition == null)
            {
                throw new Exception("Condition is invalid.");
            }

            List<T> results = new List<T>();
            foreach (KeyValuePair<int, T> dataRow in m_DataSet)
            {
                if (condition(dataRow.Value))
                {
                    results.Add(dataRow.Value);
                }
            }

            return results.ToArray();
        }

        public void GetDataRows(Predicate<T> condition, List<T> results)
        {
            if (condition == null)
            {
                throw new Exception("Condition is invalid.");
            }

            if (results == null)
            {
                throw new Exception("Results is invalid.");
            }

            results.Clear();
            foreach (KeyValuePair<int, T> dataRow in m_DataSet)
            {
                if (condition(dataRow.Value))
                {
                    results.Add(dataRow.Value);
                }
            }
        }

        public T[] GetDataRows(Comparison<T> comparison)
        {
            if (comparison == null)
            {
                throw new Exception("Comparison is invalid.");
            }

            List<T> results = new List<T>();
            foreach (KeyValuePair<int, T> dataRow in m_DataSet)
            {
                results.Add(dataRow.Value);
            }

            results.Sort(comparison);
            return results.ToArray();
        }

        public void GetDataRows(Comparison<T> comparison, List<T> results)
        {
            if (comparison == null)
            {
                throw new Exception("Comparison is invalid.");
            }

            if (results == null)
            {
                throw new Exception("Results is invalid.");
            }

            results.Clear();
            foreach (KeyValuePair<int, T> dataRow in m_DataSet)
            {
                results.Add(dataRow.Value);
            }
            results.Sort(comparison);
        }

        public T[] GetDataRows(Predicate<T> condition, Comparison<T> comparison)
        {
            if (condition == null)
            {
                throw new Exception("Condition is invalid.");
            }

            if (comparison == null)
            {
                throw new Exception("Comparison is invalid.");
            }

            List<T> results = new List<T>();
            foreach (KeyValuePair<int, T> dataRow in m_DataSet)
            {
                if (condition(dataRow.Value))
                {
                    results.Add(dataRow.Value);
                }
            }

            results.Sort(comparison);
            return results.ToArray();
        }

        public void GetDataRows(Predicate<T> condition, Comparison<T> comparison, List<T> results)
        {
            if (condition == null)
            {
                throw new Exception("Condition is invalid.");
            }

            if (comparison == null)
            {
                throw new Exception("Comparison is invalid.");
            }

            if (results == null)
            {
                throw new Exception("Results is invalid.");
            }
            results.Clear();
            foreach (KeyValuePair<int, T> dataRow in m_DataSet)
            {
                if (condition(dataRow.Value))
                {
                    results.Add(dataRow.Value);
                }
            }

            results.Sort(comparison);
        }
        public override void RemoveAllDataRows()
        {
            m_DataSet.Clear();
        }

        public override bool RemoveDataRow(int id)
        {
            if (!HasDataRow(id))
            {
                return false;
            }

            if (!m_DataSet.Remove(id))
            {
                return false;
            }
            return true;
        }
        internal override void Shutdown()
        {
            m_DataSet.Clear();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return m_DataSet.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_DataSet.Values.GetEnumerator();
        }
    }
}