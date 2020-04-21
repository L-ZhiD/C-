using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentManagerModel;
using System.Data.SqlClient;

namespace StudentManagerDAL
{
    public class AdminServer
    {
        //管理员登录
        public Admins GetAdmins(Admins adm)
        {
            //string sql = string.Format("SELECT * FROM Admins WHERE LoginId={0} AND LoginPwd='{1}'",adm.LoginId,adm.LoginPwd);
            string sql = string.Format("SELECT * FROM Admins WHERE LoginId={0}", adm.LoginId);
            SqlDataReader reader = DBHelper.SQLHelper.GetReader(sql);
            Admins use = null;
            while (reader.Read())
            {
                use = new Admins()
                {
                    AdminName = reader["AdminName"].ToString(),
                    LoginId = Convert.ToInt32(reader["LoginId"]),
                    LoginPwd = reader["LoginPwd"].ToString()
                };
            }
            reader.Close();
            return use;
        }
        /// <summary>
        /// 添加注册老师
        /// </summary>
        /// <returns></returns>
        public int ADDAdmins(Admins adm)
        {
            string str = string.Format("INSERT INTO Admins VALUES('{0}','{1}')", adm.LoginPwd, adm.AdminName);
            int a = DBHelper.SQLHelper.ExecuteNonQurey(str);
            /* if (a>0)
             {

             }*/
            return a;

        }

    }
}
