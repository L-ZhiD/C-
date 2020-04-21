using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagerModel
{
    /// <summary>
    /// 打卡实体对象
    /// </summary>
     [Serializable]
    public class Attendance
    {/// <summary>
    /// 编号
    /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 指纹编号
        /// </summary>
        public int CardNo { get; set; }
        /// <summary>
        /// 打卡时间
        /// </summary>
        public DateTime AUpdateTime { get; set; }
    }
}
