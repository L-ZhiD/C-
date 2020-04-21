using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentManagerModel.ObjExt;
using StudentManagerModel;
using StudentManagerDAL;
using System.Data.SqlClient;

namespace StudentManagerDAL
{
    public class StudentClassServer
    {
        /// <summary>
        /// 查询所有班级
        /// </summary>
        /// <returns></returns>
        public List<StudentClass> GetClasses()
        {
            string sql = "SELECT * FROM StudentClass";
            SqlDataReader reader = DBHelper.SQLHelper.GetReader(sql);
            List<StudentClass> list = new List<StudentClass>();
            while (reader.Read())
            {
                list.Add(new StudentClass()
                {
                    ClassId = Convert.ToInt32(reader["ClassId"]),
                    ClassName = reader["ClassName"].ToString()
                });
            }
            reader.Close();
            return list;
        }
        /// <summary>
        ///根据班级名称获取对应的Id
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetClassIdByName(string name)
        {
            string sql = "SELECT ClassId from StudentClass Where ClassName='" + name + "'";
            object res = DBHelper.SQLHelper.ExecuteScalar(sql);
            if (res == null)
            {
                return -1;
            }
            else
            {

                return Convert.ToInt32(res);
            }
        }

    }
}
