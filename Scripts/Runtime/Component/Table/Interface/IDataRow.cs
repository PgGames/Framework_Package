using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGFrammework.Table
{
    /// <summary>
    /// 数据表行接口。
    /// </summary>
    public interface IDataRow
    {
        /// <summary>
        /// 获取数据表行的编号。
        /// </summary>
        int Id
        {
            get;
        }

        /// <summary>
        /// 解析数据表行。
        /// </summary>
        /// <param name="dataRowString">要解析的数据表行字符串。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>是否解析数据表行成功。</returns>
        bool ParseDataRow(string dataRowString, object userData);
    }
}
