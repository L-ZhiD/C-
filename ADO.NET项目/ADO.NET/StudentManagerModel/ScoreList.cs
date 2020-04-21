using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagerModel
{
    /// <summary>
    /// 成绩实体对象
    /// </summary>
    [Serializable]
    public class ScoreList
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 学号
        /// </summary>
        public int StudentID { get; set; }
        /// <summary>
        /// C#成绩
        /// </summary>
        public int CSharp { get; set; }
        /// <summary>
        /// SQL成绩
        /// </summary>
        public int SQLServerDB { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
       /// <summary>
        /// 姓名
        /// </summary>
        public string StudentName { get; set; }
        /// <summary>
        /// 班级名称
        /// </summary>
        public string ClassName { get; set; }
        public DateTime ScoreUpdateTime { get; set; }
    }
}
