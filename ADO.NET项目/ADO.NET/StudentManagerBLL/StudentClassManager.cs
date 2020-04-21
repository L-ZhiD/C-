using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentManagerModel;
using StudentManagerDAL;
using StudentManagerModel.ObjExt;

namespace StudentManagerBLL
{
    public class StudentClassManager
    {
        StudentClassServer server = new StudentClassServer();
        public List<StudentClass> GetClasses()
        {
            return server.GetClasses();
        }
    }
}
