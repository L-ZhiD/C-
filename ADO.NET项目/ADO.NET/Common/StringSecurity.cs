using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace Common
{
    public class StringSecurity
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5Encrypt(string str) 
        {
            str += "?><Inlett";
            StringBuilder builder = new StringBuilder();
            //using作用设置这个md5对象在之后的花括号中使用时绝对不会丢失
            using (MD5 md5 = MD5.Create())
            {
                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
                foreach (byte item in data)
                {
                    builder.Append(item.ToString("X2"));
                }
            }
            return builder.ToString();
        } 
    }
}
