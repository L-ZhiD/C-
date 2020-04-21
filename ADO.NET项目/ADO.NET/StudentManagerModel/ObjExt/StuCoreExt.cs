using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagerModel.ObjExt
{
    /// <summary>
    /// 此类包含学员的成绩全部信息
    /// </summary>
    public class StuCoreExt : ScoreList
    {
        /// <summary>
        /// 学生名
        /// </summary>
        public string StudentName { get; set; }
        /// <summary>
        /// 班级名
        /// </summary>
        public string ClassName { get; set; }
    }
}
