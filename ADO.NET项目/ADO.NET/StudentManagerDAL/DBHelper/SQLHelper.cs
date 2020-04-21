using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace StudentManagerDAL.DBHelper
{
    class SQLHelper
    {
        static string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        /// <summary>
        /// 执行增,删,改操作
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int ExecuteNonQurey(string sql) 
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd
                = new SqlCommand(sql,con);
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //计入系统日志
                throw ex;
            }
            finally 
            {
                con.Close();
            }
        }
        /// <summary>
        /// 执行单一结果查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql) 
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd
                = new SqlCommand(sql, con);
            try
            {
                con.Open();
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                //计入系统日志
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
        /// <summary>
        /// 查询一张表,每行获取
        /// </summary>
        /// <param name="sql">SQL代码</param>
        /// <returns></returns>
        public static SqlDataReader DataReader(string sql)
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(sql, con);
            try
            {
                con.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 查询一张表Datatable
        /// </summary>
        /// <param name="sql">SQL代码</param>
        /// <returns></returns>
        public static DataTable DataTable(string sql)
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(sql, con);
            DataTable tab = new DataTable();
            try
            {
                con.Open();
                SqlDataAdapter sad = new SqlDataAdapter(cmd);
                sad.Fill(tab);
                return tab;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }
        /// <summary>
        /// 查询结果用DataReader读取
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static SqlDataReader GetReader(string sql) 
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd
                = new SqlCommand(sql, con);
            try
            {
                con.Open();
                //不需要手动关闭con,当DataReader关闭时,con自动跟着关闭
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                con.Close();
                //计入系统日志
                throw ex;
            }
        }
        /// <summary>
        /// 查询结果返回Dataset
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(string sql)
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(sql, con);
            DataSet set = new DataSet();
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(set);
                return set;
            }
            catch (Exception ex)
            {
                //计入系统日志
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
        /// <summary>
        /// 处理事务问题的SQL执行
        /// </summary>
        /// <param name="sqlList"></param>
        /// <returns></returns>
        public static int UpdateByTran(List<string> sqlList) 
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand();//SQL集合
            cmd.Connection = con;
            try
            {
                //开启数据库的连接
                con.Open();
                //开始执行事务
                cmd.Transaction = con.BeginTransaction();
                int result = 0;
                //获取每一个SQL代码,保证每一条SQL 代码执行成功才会到Commit,SQL语句中只要有一条抛异常都会进入catch中
                foreach (string sql in sqlList)
                {
                    cmd.CommandText = sql;
                    //对每条SQL代码的执行结构进行接收
                    result += cmd.ExecuteNonQuery();//受影响行数统计数量
                }
                cmd.Transaction.Commit();//提交事务
                return result;
            }
            catch (Exception ex)
            {
                if (cmd.Transaction != null)
                {
                    cmd.Transaction.Rollback();//进行回滚
                }
                throw new Exception("调用事务更新方法时出现异常!"+ex.Message);
            }
            finally 
            {
                if (cmd.Transaction != null)
                {
                    cmd.Transaction = null;
                }
                con.Close();
            }
        }
    }
}
