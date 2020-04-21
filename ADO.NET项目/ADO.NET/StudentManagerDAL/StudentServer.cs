using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentManagerModel;
using StudentManagerDAL;
using StudentManagerModel.ObjExt;
using System.Data.SqlClient;
using System.Data;
using Common;

namespace StudentManagerDAL
{
    /// <summary>
    /// 学生管理的数据访问
    /// </summary>
    public class StudentServer
    {
        public List<StudentExt> GetStudents(int cid)
        {
            string sql = "SELECT StudentID,StudentName,StudentSex,Birthday,StudentIdNo,CardNo,StuImage,Age,PhoneNumber,StudentAddress,StudentClass.ClassName FROM Students INNER JOIN StudentClass ON StudentClass.ClassId=Students.ClssID WHERE StudentClass.ClassId=" + cid;
            SqlDataReader reader = DBHelper.SQLHelper.GetReader(sql);
            List<StudentExt> list = DataReturnObj(reader);
            reader.Close();
            return list;
        }
        public DataTable GetDataTable(int cid)
        {
            string sql = "SELECT StudentID ,StudentName,StudentSex,Birthday,StudentIdNo,CardNo,Age,PhoneNumber,StudentAddress,StudentClass.ClassName FROM Students INNER JOIN StudentClass ON StudentClass.ClassId=Students.ClassID WHERE StudentClass.ClassId=" + cid;
            DataTable table = DBHelper.SQLHelper.GetDataSet(sql).Tables[0];
            return table;
        }

        private static List<StudentExt> DataReturnObj(SqlDataReader reader)
        {
            List<StudentExt> list = new List<StudentExt>();
            while (reader.Read())
            {
                list.Add(new StudentExt()
                {
                    StudentID = Convert.ToInt32(reader["StudentID"]),
                    StudentName = reader["StudentName"].ToString(),
                    StudentSex = reader["StudentSex"].ToString(),
                    Birthday = Convert.ToDateTime(reader["Birthday"]),
                    StudentIdNo = reader["StudentIdNo"].ToString(),
                    CardNo = reader["CardNo"].ToString(),
                    StuImage = reader["StuImage"].ToString(),
                    Age = Convert.ToInt32(reader["Age"]),
                    PhoneNumber = reader["PhoneNumber"].ToString(),
                    StudentAddress = reader["StudentAddress"].ToString(),
                    ClassName = reader["ClassName"].ToString(),
                });
            }

            return list;
        }
      
        /// <summary>
        /// 根据ID或者名称模糊查询
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public List<StudentExt> GetStudentByldOrName(string target)
        {
            string sql = string.Format("SELECT StudentID,StudentName,StudentSex,Birthday,StudentIdNo,CardNo,StuImage,Age,PhoneNumber,StudentAddress,StudentClass.ClassName FROM Students INNER JOIN StudentClass ON StudentClass.ClassId = Students.ClssID WHERE StudentID LIKE'%{0}%'OR StudentName LIKE '%{0}%'", target);
            SqlDataReader reader = DBHelper.SQLHelper.GetReader(sql);
            List<StudentExt> list = DataReturnObj(reader);
            reader.Close();
            return list;
        }
        public StudentExt GetStudentById(int id) 
        {
            string sql = string.Format("SELECT StudentID,StudentName,StudentSex,Birthday,StudentIdNo,CardNo,StuImage,Age,PhoneNumber,StudentAddress,Students.ClssID,StudentClass.ClassName FROM Students INNER JOIN StudentClass ON StudentClass.ClassId = Students.ClssID WHERE StudentID = {0} ",id);
            SqlDataReader reader = DBHelper.SQLHelper.GetReader(sql);
            StudentExt student = null;
            while(reader.Read())
            {
                student = new StudentExt()
                {
                    StudentID = Convert.ToInt32(reader["StudentID"]),
                    StudentName = reader["StudentName"].ToString(),
                    StudentSex = reader["StudentSex"].ToString(),
                    Birthday = Convert.ToDateTime(reader["Birthday"]),
                    StudentIdNo = reader["StudentIdNo"].ToString(),
                    CardNo = reader["CardNo"].ToString(),
                    StuImage = reader["StuImage"].ToString(),
                    Age = Convert.ToInt32(reader["Age"]),
                    PhoneNumber = reader["PhoneNumber"].ToString(),
                    StudentAddress = reader["StudentAddress"].ToString(),
                    ClassName = reader["ClassName"].ToString(),
                    ClssID = Convert.ToInt32(reader["ClssID"])
                };
            }
            return student;
        }

        public int UpdateStudentInfor(StudentExt student) 
        {
            string sql= string.Format("UPDATE Students SET StudentName='{0}',StudentSex='{1}',Birthday='{2}',StudentIdNo='{3}',CardNo='{4}',StuImage='{5}',Age='{6}',PhoneNumber='{7}',StudentAddress='{8}',ClssID='{9}' WHERE StudentID='{10}'", student.StudentName, student.StudentSex, student.Birthday, student.StudentIdNo, student.CardNo, student.StuImage, student.Age, student.PhoneNumber, student.StudentAddress, student.ClssID, student.StudentID);
            return DBHelper.SQLHelper.ExecuteNonQurey(sql);
        }
        public int DeleteStudentById(int sid)
        {
            string sql = "DELETE FROM Students WHERE StudentId=" + sid;
            return DBHelper.SQLHelper.ExecuteNonQurey(sql);
        }
        /// <summary>
        /// 添加学生
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public int AddStudent(StudentExt student)
        {
            string sql = string.Format(" INSERT INTO Students VALUES('{0}','{1}','{2}','{3}','{4}',{5},'{6}','{7}','{8}','{9}')", student.StudentName, student.StudentSex, student.Birthday, student.StudentIdNo, student.CardNo, student.StuImage, student.Age, student.PhoneNumber, student.StudentAddress, student.ClssID);
            return DBHelper.SQLHelper.ExecuteNonQurey(sql);

        }
        StudentClassServer classServer = new StudentClassServer();
        /// <summary>
        /// 从Excel文件中读取数据
        /// </summary>
        /// <returns></returns>
        public List<StudentExt> GetStudentByExcel(string path)
        {
            List<StudentExt> list = new List<StudentExt>();
            string sql = string.Format("select * from [{0}$] ", Common.ImportOrExportData.SheetName(path));
            DataSet ds = Common.OleDbHelper.GetDataSet(sql, path);
            DataTable dt = ds.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                string id = row["身份证号"].ToString();
                list.Add(new StudentExt()
                {
                    StudentName = row["姓名"].ToString(),
                    StudentSex = row["性别"].ToString(),
                    Birthday = Common.GetValueById.GetBirthday(id),
                    Age = Common.GetValueById.GetAge(Common.GetValueById.GetBirthday(id)),
                    CardNo = row["考勤卡号"].ToString(),
                    StudentIdNo = id,
                    PhoneNumber = row["电话号码"].ToString(),
                    StudentAddress = row["家庭住址"].ToString(),
                    ClassName = row["班级"].ToString(),
                    ClssID = classServer.GetClassIdByName(row["班级名称"].ToString())
                }) ;
            }
            return list;
        }

        public int InsertStudent(StudentExt stu)
        {
            string sql = string.Format("INSERT INTO Students(StudentName,Gender,Birthday,StudentIdNo,CardNo,Age,PhoneNumber,StudentAddress,ClassId) VALUES('{0}', '{1}', '{2}', '{3}', '{4}', {5}, '{6}', '{7}', {8})", stu.StudentName, stu.StudentSex, stu.Birthday, stu.StudentIdNo, stu.CardNo, stu.Age, stu.PhoneNumber, stu.StudentAddress, stu.ClssID);
            return DBHelper.SQLHelper.ExecuteNonQurey(sql);
        }
        public int ImportStudentData(List<StudentExt> list) 
        {
            List<string> sqllist = new List<string>();
            foreach (StudentExt stu in list)
            {
                string sql = string.Format("INSERT INTO Students(StudentName,Gender,Birthday,StudentIdNo,CardNo,Age,PhoneNumber,StudentAddress,ClassId) VALUES('{0}', '{1}', '{2}', '{3}', '{4}', {5}, '{6}', '{7}', {8})", stu.StudentName, stu.StudentSex, stu.Birthday, stu.StudentIdNo, stu.CardNo, stu.Age, stu.PhoneNumber, stu.StudentAddress, stu.ClssID);
                sqllist.Add(sql);
            }
            return DBHelper.SQLHelper.UpdateByTran(sqllist);
        }

        public int CheckStuId(string id)
        {
            string sql = "SELECT COUNT(*) FROM Students WHERE StudentIdNo='" + id + "'";
            object res = DBHelper.SQLHelper.ExecuteScalar(sql);
            return (int)res;
        }
        /// <summary>
        /// 通过考勤号查询
        /// </summary>
        /// <param name="stexe"></param>
        /// <returns></returns>
        public DataTable GetStuByCard(int stexe)
        {
            string sql = "SELECT *FROM Students WHERE CardNO=" + stexe + "";
            DataTable tabl = DBHelper.SQLHelper.DataTable(sql);
            return tabl;
        }

    }
}
