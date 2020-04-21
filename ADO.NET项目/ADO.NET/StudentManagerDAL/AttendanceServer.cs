using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentManagerModel;
using StudentManagerModel.ObjExt;
using StudentManagerDAL;
using System.Data.SqlClient;
using System.Data;

namespace StudentManagerDAL
{
    public class AttendanceServer
    {
        /// <summary>
        /// 通过班级号查询考勤表
        /// </summary>
        /// <param name="cId"></param>
        /// <returns></returns>
        public List<AttInforExt> GetAttInfors(int cid)
        {
            string sql = "SELECT StudentID,StudentName,CardNO,ClassID,ClassName,DTime FROM AttInfor WHERE ClassID=" + cid + "";  //通过创建的视图表的班级号查询
            SqlDataReader reader = DBHelper.SQLHelper.DataReader(sql);
            List<AttInforExt> list = DataReadscore(reader);
            return list;
        }
        private List<AttInforExt> DataReadscore(SqlDataReader reader)
        {
            List<AttInforExt> attinfolist = new List<AttInforExt>();
            while (reader.Read())
            {
                attinfolist.Add(new AttInforExt()
                {
                    StudentID = Convert.ToInt32(reader["StudentID"]),//学号
                    StudentName = reader["StudentName"].ToString(),//姓名
                    CardNo = Convert.ToInt32(reader["CardNO"]),//考勤号
                    ClassName = reader["ClassName"].ToString(),//班级
                    AUpdateTime = Convert.ToDateTime(reader["DTime"])//录入时间
                });
            }
            return attinfolist;
        }
        /// <summary>
        /// 根据学号或姓名查询
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public List<AttInforExt> GetAttinforExts(string target)
        {
            string sql = string.Format("SELECT StudentID,StudentName,CardNO,ClassID,ClassName,DTime FROM AttInfor WHERE StudentID LIKE '%{0}%'OR StudentName LIKE'%{0}%'", target);
            SqlDataReader reader = DBHelper.SQLHelper.DataReader(sql);
            List<AttInforExt> list = DataReadscore(reader);
            return list;
        }
        /// <summary>
        /// 添加考勤
        /// </summary>
        /// <param name="attInfor"></param>
        /// <returns></returns>
        public int AddattInfor(Attendance attInfor)
        {
            string sql = string.Format("INSERT Attendance VALUES ({0},'{1}')", attInfor.CardNo, attInfor.AUpdateTime);
            return DBHelper.SQLHelper.ExecuteNonQurey(sql);
        }
        /// <summary>
        /// 通过班级编号获取考勤表
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public DataTable GetAttInByCId(int cid)
        {
            string sql = "SELECT StudentID,StudentName,CardNO,ClassID,ClassName,DTime FROM AttInfor WHERE ClassID=" + cid + "";  //通过创建的视图表的班级号查询
            return DBHelper.SQLHelper.DataTable(sql);
        }
        /// <summary>
        /// 通过班级编号，查看每个出勤次数
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public List<AttInforExt> GetAttRateByCId(int cid)
        {
            string sql = "EXEC AttRate " + cid + "";  //同调用存储过程
            SqlDataReader reader = DBHelper.SQLHelper.DataReader(sql);
            List<AttInforExt> list = new List<AttInforExt>();
            while (reader.Read())
            {
                list.Add(new AttInforExt()
                {
                    StudentID = Convert.ToInt32(reader["StudentID"]),//学号
                    StudentName = reader["StudentName"].ToString(),//姓名
                    CardNo = Convert.ToInt32(reader["CardNO"]),//考勤号
                    AttRate = Convert.ToInt32(reader["AttRate"]), //出勤次数
                });
            };
            return list;
        }
    }
}
