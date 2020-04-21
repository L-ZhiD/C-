using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using StudentManagerModel;
using StudentManagerModel.ObjExt;
namespace StudentManagerDAL
{
    /// <summary>
    /// 处理学生成绩表数据库中数据
    /// </summary>
    public class ScoreServer
    {
        /// <summary>
        /// 通过学号查询成绩信息
        /// </summary>
        /// <param name="id">学号</param>
        /// <returns></returns>
        public StuCoreExt GetScoreById(int id)
        {
            string sql = "SELECT ScoreList.StudentID, StudentName,StudentClass.ClassName,CSharp,SQLServerDB,ScoreUpdateTime  FROM ScoreList JOIN Students ON ScoreList.StudentID=Students.StudentID JOIN StudentClass ON Students.ClassID=StudentClass.ClassID WHERE ScoreList.StudentID=" + id + "";
            SqlDataReader reader = DBHelper.SQLHelper.DataReader(sql);
            StuCoreExt score = null;
            while (reader.Read())
            {
                score = new StuCoreExt()
                {
                    StudentID = Convert.ToInt32(reader["StudentID"]),
                    StudentName = reader["StudentName"].ToString(),
                    ClassName = reader["ClassName"].ToString(),
                    CSharp = Convert.ToInt32(reader["CSharp"]),
                    SQLServerDB = Convert.ToInt32(reader["SQLServerDB"]),
                    ScoreUpdateTime = Convert.ToDateTime(reader["ScoreUpdateTime"])
                };
            }
            return score;
        }

        /// <summary>
        /// 通过班级名获取对应的成绩数据
        /// </summary>
        /// <param name="cid">班级id</param>
        /// <returns></returns>
        public List<StuCoreExt> GetScore(int cid)
        {
            string sql = "SELECT ScoreList.StudentID, StudentName,StudentClass.ClassName,CSharp,SQLServerDB,ScoreUpdateTime  FROM ScoreList JOIN Students ON ScoreList.StudentID=Students.StudentID JOIN StudentClass ON Students.ClassID=StudentClass.ClassID WHERE StudentClass.ClassID=" + cid + "";
            SqlDataReader reader = DBHelper.SQLHelper.DataReader(sql);
            List<StuCoreExt> list = DataReadscore(reader);
            return list;
        }
        private List<StuCoreExt> DataReadscore(SqlDataReader reader)
        {
            List<StuCoreExt> scorelist = new List<StuCoreExt>();
            while (reader.Read())
            {
                scorelist.Add(new StuCoreExt()
                {
                    StudentID = Convert.ToInt32(reader["StudentID"]),
                    StudentName = reader["StudentName"].ToString(),
                    ClassName = reader["ClassName"].ToString(),
                    CSharp = Convert.ToInt32(reader["CSharp"]),
                    SQLServerDB = Convert.ToInt32(reader["SQLServerDB"]),
                    ScoreUpdateTime = Convert.ToDateTime(reader["ScoreUpdateTime"])
                });
            }
            return scorelist;
        }

        /// <summary>
        /// 通过学号或姓名的部分数据查询
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public List<StuCoreExt> GetStuscoreExts(string target)
        {
            string sql = string.Format("SELECT ScoreList.StudentID, StudentName,StudentClass.ClassName,CSharp,SQLServerDB,ScoreUpdateTime  FROM ScoreList JOIN Students ON ScoreList.StudentID=Students.StudentID JOIN StudentClass ON Students.ClassID=StudentClass.ClassID WHERE ScoreList.StudentID LIKE '%{0}%'OR StudentName LIKE'%{0}%'", target);
            SqlDataReader reader = DBHelper.SQLHelper.DataReader(sql);
            List<StuCoreExt> list = DataReadscore(reader);
            return list;
        }
        /// <summary>
        /// 修改学员成绩
        /// </summary>
        /// <param name="scor">成绩对象</param>
        /// <returns></returns>
        public int UpScore(StuCoreExt scor)
        {
            string sql = string.Format("UPDATE ScoreList SET CSharp={0},SQLServerDB={1} WHERE StudentID={2}", scor.CSharp, scor.SQLServerDB, scor.StudentID);
            return DBHelper.SQLHelper.ExecuteNonQurey(sql);
        }
        /// <summary>
        /// 删除学员成绩
        /// </summary>
        /// <param name="sco"></param>
        /// <returns></returns>
        public int DeleScore(int id)
        {
            string sql = "DELETE ScoreList WHERE StudentID=" + id + "";
            return DBHelper.SQLHelper.ExecuteNonQurey(sql);
        }
        /// <summary>
        /// 添加学员成绩
        /// </summary>
        /// <param name="sco"></param>
        /// <returns></returns>
        public int AddScore(StuCoreExt sco)
        {
            string sql = string.Format("INSERT ScoreList VALUES ({0},{1},{2},'{3}')", sco.StudentID, sco.CSharp, sco.SQLServerDB, sco.ScoreUpdateTime);
            return DBHelper.SQLHelper.ExecuteNonQurey(sql);
        }
        /// <summary>
        /// 通过班级编号，获取班级对应的学员成绩
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public DataTable GetScoreByCId(int cid)
        {
            string sql = "SELECT ScoreList.StudentID, StudentName,StudentClass.ClassName,CSharp,SQLServerDB,ScoreUpdateTime  FROM ScoreList JOIN Students ON ScoreList.StudentID=Students.StudentID JOIN StudentClass ON Students.ClassID=StudentClass.ClassID WHERE StudentClass.ClassID=" + cid + "";
            return DBHelper.SQLHelper.DataTable(sql);
        }
    }
}
