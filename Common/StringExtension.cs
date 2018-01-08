using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SZHome.Common
{
    public static class StringExtension
    {
        /// <summary>
        /// 获取int集合
        /// </summary>
        /// <param name="listStr">list字符串</param>
        /// <returns></returns>
        public static List<int> ToIntList(this string listStr)
        {
            List<int> list = new List<int>();
            if (!string.IsNullOrWhiteSpace(listStr))
            {
                string[] cols = listStr.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var col in cols)
                {
                    int value = 0;
                    if (int.TryParse(col, out value))
                    {
                        list.Add(value);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 获取int集合
        /// </summary>
        /// <param name="listStr">list字符串</param>
        /// <param name="mCharSplit">分割字符</param>
        /// <returns></returns>
        public static List<string> ToStringList(this string listStr)
        {
            List<string> list = new List<string>();
            if (!string.IsNullOrWhiteSpace(listStr))
            {
                string[] cols = listStr.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var col in cols)
                {
                    list.Add(col.Trim());
                }
            }
            return list;
        }

        /// <summary>
        /// 获取int集合
        /// </summary>
        /// <param name="listStr">list字符串</param>
        /// <param name="mCharSplit">分割字符</param>
        /// <returns></returns>
        public static List<string> ToStringList(this string listStr, char mCharSplit)
        {
            List<string> list = new List<string>();
            if (!string.IsNullOrWhiteSpace(listStr))
            {
                string[] cols = listStr.Split(new char[] { mCharSplit, ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var col in cols)
                {
                    list.Add(col.Trim());                    
                }
            }
            return list;
        }

        /// <summary>
        /// 获取Int
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToInt(this string value)
        {
            return SZHomeDLL.ConvertHelper.StringToInt(value);
        }

        /// <summary>
        /// 去除null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string TrimNull(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }
            else
            {
                return value.Trim();
            }
        }

        /// <summary>
        /// 获取匿名字符串,只取第一位,后面全部转*
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetAnonymString(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            { return value; }

            int length = value.Length - 1;
            string anonym = value.Substring(0, 1);
            return anonym.PadRight(length, '*');
        }


    }
}
