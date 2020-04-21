using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class SQLHelper
    {
        static string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        /// <summary>
        /// 返回受影响行数
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="sp"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string procName, SqlParameter[] sp)
        {
            SqlConnection sqlcon = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlcon;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procName;
            if (sp!=null)
            {
                cmd.Parameters.AddRange(sp);
            }
            try
            {
                sqlcon.Open();
                int res = cmd.ExecuteNonQuery();
                return res;
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                sqlcon.Close();
            }
        }
        /// <summary>
        /// 返回单一结果
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="sp"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string procName, SqlParameter[] sp)
        {
            SqlConnection sqlcon = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlcon;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procName;
            if (sp != null)
            {
                cmd.Parameters.AddRange(sp);
            }
            try
            {
                sqlcon.Open();
                object res = cmd.ExecuteScalar();
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                sqlcon.Close();
            }
        }
        /// <summary>
        /// 返回DataReader
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="sp"></param>
        /// <returns></returns>
        public static SqlDataReader GetDataReader(string procName, SqlParameter[] sp)
        {
            SqlConnection sqlcon = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlcon;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procName;
            if (sp != null)
            {
                cmd.Parameters.AddRange(sp);
            }
            try
            {
                sqlcon.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 返回一个数据表
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="sp"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string procName, SqlParameter[] sp)
        {
            SqlConnection sqlcon = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlcon;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procName;
            if (sp != null)
            {
                cmd.Parameters.AddRange(sp);
            }
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                sqlcon.Close();
            }
        }
        /// <summary>
        /// 处理一个事务
        /// </summary>
        /// <returns></returns>
        public static bool UpdateByTran(List<string> procList, List<SqlParameter[]> pslist)
        {
            SqlConnection sqlcon = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlcon;
            try
            {
                sqlcon.Open();
                cmd.Transaction = sqlcon.BeginTransaction();
                foreach (string procName in procList)
                {
                    cmd.CommandText = procName;
                    if (pslist[procList.IndexOf(procName)] != null)
                    {
                        cmd.Parameters.AddRange(pslist[procList.IndexOf(procName)]);
                    }
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                cmd.Transaction.Commit();//所有的存储过程执行完没有异常提交
                return true;
            }
            catch (Exception ex)
            {
                if (cmd.Transaction != null)
                {
                    cmd.Transaction.Rollback();
                }
                return false;
            }
            finally
            {
                if (cmd.Transaction != null)
                {
                    cmd.Transaction = null;
                }
                sqlcon.Close();
            }
        }
    }
}
