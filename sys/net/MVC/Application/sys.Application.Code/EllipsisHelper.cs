using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sys.Application.Code
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：张念
    /// 日 期：2019.05.28
    /// 描 述：字符串处理累
    /// </summary>
    public static class EllipsisHelper
    {
        /// <summary>
        /// 字符串截取
        /// </summary>
        /// <param name="str">目标字符串</param>
        /// <param name="length">字符串截取长度</param>
        /// <returns>String</returns>
        public static string Ellipsis(string str, int length)
        {
            string result = string.Empty;
            if (!string.IsNullOrWhiteSpace(str))
            {
                if (str.Length > length)
                {
                    result = str.Substring(0, length);
                }
                else
                {
                    result = str;
                }
            }
            return result;
        }
    }
}
