using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagerModel.ObjExt
{
    public class AttInforExt : Attendance
    {
        /// <summary>
        /// 学号
        /// </summary>
        public int StudentID { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string StudentName { get; set; }
        /// <summary>
        /// 班级名
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 出勤次数
        /// </summary>
        public int AttRate { get; set; }
    }
}
