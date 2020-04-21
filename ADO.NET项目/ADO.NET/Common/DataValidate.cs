using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Common
{
    public class DataValidate
    {
        /// <summary>
        /// 验证正整数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsInteger(string str)
        {
            Regex reg = new Regex(@"^[1-9]\d*$");
            return reg.IsMatch(str);
        }
        /// <summary>
        /// 身份证验证
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsIdentitycard(string str)
        {

            Regex regex = new Regex(@"^([1 - 6][1 - 9]|50)\d{4}(18|19|20)\d{2}((0[1-9])|10|11|12)(([0 - 2][1 - 9])|10|20|30|31)\d{3}[0-9Xx]$");
            return regex.IsMatch(str);
        }
        /// <summary>
        /// 手机号验证
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsPhone(string str)
        {
            Regex regex = new Regex(@"^1[3578]\d{9}$");
            return regex.IsMatch(str);
        }
        ///身份证验证、电话或手机号验证、日期验证

        public static bool IsSCard(string str)
        {
            Regex reg = new Regex(@"(^\d{ 18 }$)| (^\d{ 17} (\d | X | x)$)");
            return reg.IsMatch(str);
        }
    }
}
