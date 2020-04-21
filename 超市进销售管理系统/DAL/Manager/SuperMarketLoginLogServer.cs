using Moder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.Manager;
using System.Data.SqlClient;

namespace DAL.Manager
{
    public class SuperMarketLoginLogServer : ISuperMarketLoginLogServer
    {
        public List<LoginLogs> GetLoginLog()
        {
            string procName = "GetLoginLogs";
            SqlDataReader reader = SQLHelper.GetDataReader(procName, null);
            List<LoginLogs> list = new List<LoginLogs>();
            while (reader.Read())
            {
                LoginLogs logs = new LoginLogs();
                logs.LogId = Convert.ToInt32(reader["LogId"]);
                if (string.IsNullOrEmpty(reader["ExitTime"].ToString()))
                {
                    logs.ExitTime = null;
                }
                else
                {
                    logs.ExitTime = Convert.ToDateTime(reader["ExitTime"]);
                }
                logs.LoginId = Convert.ToInt32(reader["LoginId"]);
                logs.SPName = reader["SPName"].ToString();
                logs.ServerName = reader["ServerName"].ToString();
                logs.LoginTime = Convert.ToDateTime(reader["LoginTime"]);
                list.Add(logs);
            }
            reader.Close();
            return list;
        }

        public List<LoginLogs> GetLoginLogBy(DateTime starttime, DateTime endTime, string wheres, int check)
        {
            string procName = "GetLoginLogBy";
            SqlParameter[] sp =
            {
                new SqlParameter("@startTime",starttime),
                new SqlParameter("@endTime",endTime),
                new SqlParameter("@where",wheres),
                new SqlParameter("@check",check)
            };
            SqlDataReader reader = SQLHelper.GetDataReader(procName, sp);
            List<LoginLogs> list = new List<LoginLogs>();
            while (reader.Read())
            {
                LoginLogs logs = new LoginLogs();
                logs.LogId = Convert.ToInt32(reader["LogId"]);
                if (string.IsNullOrEmpty(reader["ExitTime"].ToString()))
                {
                    logs.ExitTime = null;
                }
                else
                {
                    logs.ExitTime = Convert.ToDateTime(reader["ExitTime"]);
                }
                logs.LoginId = Convert.ToInt32(reader["LoginId"]);
                logs.SPName = reader["SPName"].ToString();
                logs.ServerName = reader["ServerName"].ToString();
                logs.LoginTime = Convert.ToDateTime(reader["LoginTime"]);
                list.Add(logs);
            }
            reader.Close();
            return list;
        }
    }
}
