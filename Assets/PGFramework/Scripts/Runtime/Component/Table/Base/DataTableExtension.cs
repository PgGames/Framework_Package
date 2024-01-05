using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PGFrammework.Table
{
    public static class DataTableExtension
    {
        /// <summary>
        /// 数据表命名空间前缀
        /// </summary>
        public const string DataRowClassPrefixName = "PGFrammework.DR";
        /// <summary>
        /// 备注行数据标识
        /// </summary>
        public const string CommentLineSeparator = "#";
        /// <summary>
        /// 表数据的行数据中的列分割符
        /// </summary>
        public static readonly char[] DataSplitSeparators = new char[] { '\t' };
        /// <summary>
        /// 表数据的内容剔除前后的符号
        /// </summary>
        public static readonly char[] DataTrimSeparators = new char[] { '\"' };
        /// <summary>
        /// 表数据的换行分割符
        /// </summary>
        public static readonly string[] DataLineSplitSeparators = new string[] { "\r\n" };

        public static readonly char[] attributeTrim = new char[] { '(', ')', '（', '）' };
        public static readonly char[] attributeSplit = new char[] { ',', '，', '|' };


        public static Color32 ParseColor32(string value)
        {
            string[] splitedValue = value.Trim(attributeTrim).Split(attributeSplit);
            return new Color32(byte.Parse(splitedValue[0]), byte.Parse(splitedValue[2]), byte.Parse(splitedValue[3]), byte.Parse(splitedValue[4]));
        }

        public static Color ParseColor(string value)
        {
            string[] splitedValue = value.Trim(attributeTrim).Split(attributeSplit);
            if (splitedValue.Length == 4)
            {
                return new Color(float.Parse(splitedValue[0]), float.Parse(splitedValue[2]), float.Parse(splitedValue[3]), float.Parse(splitedValue[4]));
            }
            else if (splitedValue.Length == 3)
            {
                return new Color(float.Parse(splitedValue[0]), float.Parse(splitedValue[2]), float.Parse(splitedValue[3]));
            }
            else
                return Color.white;
        }
        public static Quaternion ParseQuaternion(string value)
        {
            string[] splitedValue = value.Trim(attributeTrim).Split(attributeSplit);
            return new Quaternion(float.Parse(splitedValue[0]), float.Parse(splitedValue[1]), float.Parse(splitedValue[2]), float.Parse(splitedValue[3]));
        }
        public static Rect ParseRect(string value)
        {
            string[] splitedValue = value.Trim(attributeTrim).Split(attributeSplit);
            return new Rect(float.Parse(splitedValue[0]), float.Parse(splitedValue[1]), float.Parse(splitedValue[2]), float.Parse(splitedValue[3]));
        }
        public static Vector2 ParseVector2(string value)
        {
            string[] splitedValue = value.Trim(attributeTrim).Split(attributeSplit);
            return new Vector2(float.Parse(splitedValue[0]), float.Parse(splitedValue[1]));
        }
        public static Vector3 ParseVector3(string value)
        {
            string[] splitedValue = value.Trim(attributeTrim).Split(attributeSplit);
            return new Vector3(float.Parse(splitedValue[0]), float.Parse(splitedValue[1]), float.Parse(splitedValue[2]));
        }
        public static Vector4 ParseVector4(string value)
        {
            string[] splitedValue = value.Trim(attributeTrim).Split(attributeSplit);
            return new Vector4(float.Parse(splitedValue[0]), float.Parse(splitedValue[1]), float.Parse(splitedValue[2]), float.Parse(splitedValue[3]));
        }
    }
}
