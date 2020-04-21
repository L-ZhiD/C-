using Moder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Cashier
{
    /// <summary>
    /// 处理营业员管理
    /// </summary>
    public class SuperMarketSaleServer : IDAL.Cashier.ISuperMarketSaleServer
    {
        public SalesPerson SalesLogin(SalesPerson person)
        {
            string procName = "SalesLogIn";
            SqlParameter[] sp =
            {
                new SqlParameter("@LoginId", SqlDbType.Int),
                new SqlParameter("@LoginPwd", SqlDbType.NVarChar,50)
            };
            sp[0].Value = person.SalesPersonId;
            sp[1].Value = person.LoginPwd;
            SqlDataReader reader = SQLHelper.GetDataReader(procName, sp);
            SalesPerson sales = null;
            while (reader.Read())
            {
                sales = new SalesPerson()
                {
                    LoginPwd = reader["LoginPwd"].ToString(),
                    SPName = reader["SPName"].ToString(),
                    SalesPersonId = int.Parse(reader["SalesPersonId"].ToString())
                };
            }
            reader.Close();
            return sales;
        }
        public Produts GetProductWithId(string productId)
        {
            string procName = "GetProductWithId";
            SqlParameter[] sp =
            {
                new SqlParameter("@productId", SqlDbType.NVarChar,50)
            };
            sp[0].Value = productId;
            SqlDataReader reader = SQLHelper.GetDataReader(procName, sp);
            Produts produt = null;
            while (reader.Read())
            {
                produt = new Produts()
                {
                    ProductId = reader["ProductId"].ToString(),
                    ProductName = reader["ProductName"].ToString(),
                    UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                    Unit = reader["Unit"].ToString(),
                    Discount = Convert.ToInt32(reader["Discount"]),
                    CategoryId = Convert.ToInt32(reader["CategoryId"]),
                    CategoryName = reader["CategoryName"].ToString()
                };
            }
            reader.Close();
            return produt;
        }

        public DateTime GetSysTime()
        {
            string procName = "GetSysTime";
            return Convert.ToDateTime(SQLHelper.ExecuteScalar(procName, null));
        }

       

        public int WriteSalesExitLog(int logid)
        {
            string procName = "ExitSysWriteLog";
            SqlParameter[] sp =
            {
                new SqlParameter("@LogId", SqlDbType.Int)
            };
            sp[0].Value = logid;
            return SQLHelper.ExecuteNonQuery(procName, sp);
        }
        // <summary>
        /// 日志记录成功返回本次日志的ID号
        /// </summary>
        /// <param name="logs"></param>
        /// <returns></returns>
        public int WriteSalesLog(LoginLogs logs)
        {
            string procName = "WriteLog";
            SqlParameter[] sp =
            {
                new SqlParameter("@LoginId", SqlDbType.Int),
                new SqlParameter("@SPName", SqlDbType.NVarChar,50),
                new SqlParameter("@ServerName", SqlDbType.NVarChar,50)
            };
            sp[0].Value = logs.LoginId;
            sp[1].Value = logs.SPName;
            sp[2].Value = logs.ServerName;
            object res = SQLHelper.ExecuteScalar(procName, sp);
            if (res == null)
            {
                return -1;
            }
            return int.Parse(res.ToString());
        }
    }
}
