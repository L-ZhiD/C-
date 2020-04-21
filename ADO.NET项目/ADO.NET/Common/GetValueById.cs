using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{    /// <summary>
     /// 通过身份号获取某些数据功能
     /// </summary>
    public class GetValueById
    {
        /// <summary>
        /// 通过身份证号获取生日
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DateTime GetBirthday(string id)
        {
            string date = id.Substring(6, 8);
            string y = date.Substring(0, 4);
            string m = date.Substring(4, 2);
            string d = date.Substring(6, 2);
            string bir = y + "-" + m + "-" + d;
            return Convert.ToDateTime(bir);
        }
        /// <summary>
        /// 通过出生日期计算年龄
        /// </summary>
        /// <param name="birthday"></param>
        /// <returns></returns>
        public static int GetAge(DateTime birthday)
        {
            return DateTime.Now.Year - birthday.Year;
        }
    }
}
