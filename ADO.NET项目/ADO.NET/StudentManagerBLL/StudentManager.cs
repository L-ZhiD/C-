using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentManagerDAL;
using StudentManagerModel.ObjExt;
using StudentManagerModel;
using System.Data;


namespace StudentManagerBLL
{
    /// <summary>
    /// 学生管理的业务逻辑
    /// </summary>
    public class StudentManager
    {
        StudentServer server = new StudentServer();
        public List<StudentExt> GetStudents(int cid)
        {
            return server.GetStudents(cid);
        }
        public DataTable GetDataTable(int cid)
        {
            return server.GetDataTable(cid);
        }
        /// <summary>
        /// 整个表格需要这个数据Data...的表
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public List<StudentExt> GetStudentsByIdOrName(string target)
        {
            return server.GetStudentByldOrName(target);
        }
        public StudentExt GetStudentById(int id) 
        {
            return server.GetStudentById(id);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public bool UpdateStudentInfor(StudentExt student) 
        {
            if (server.UpdateStudentInfor(student)<=0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public bool DeleteStudentById(int sid)
        {
            if (server.DeleteStudentById(sid) <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool AddStudent(StudentExt student) 
        {
            if (server.AddStudent(student) <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 把Excel库的每一个东西赋给StudentsExt类型的list范性
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<StudentExt> GetStudentByExcel(string path)
        {
            return server.GetStudentByExcel(path);
        }
        //给SQL数据库导入数据（要用到数据访问层中学生类的两个方法；方法1：CheckStuId获取数据库的学生的ID值；方法2：给数据添加值）
        public int InsertStudent(StudentExt stu)
        {
            //if (server.CheckStuId(stu.StudentIdNo) > 0)
            //{
            //    return -1;
            //}
            return server.InsertStudent(stu);
        }
        public int ImportStudentData(List<StudentExt> list) 
        {
            return server.ImportStudentData(list);
        }
        public DataTable GetStuByCard(int carid)
        {
            DataTable tab = server.GetStuByCard(carid);
            return tab;
        }
    }
}
