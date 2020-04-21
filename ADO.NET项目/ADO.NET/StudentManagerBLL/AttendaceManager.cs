using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentManagerModel;
using StudentManagerModel.ObjExt;
using StudentManagerDAL;
using System.Data;

namespace StudentManagerBLL
{
    public class AttendaceManager
    {
        AttendanceServer atserver = new AttendanceServer();
        /// <summary>
        /// 通过班级名获取考勤信息
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public List<AttInforExt> GetAttInfors(int cid)
        {
            return atserver.GetAttInfors(cid);
        }
        /// <summary>
        /// 通过学号或姓名查询
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public List<AttInforExt> GetAttByStuIDorName(string target)
        {
            return atserver.GetAttinforExts(target);
        }
        /// <summary>
        /// 添加考勤
        /// </summary>
        /// <param name="atinfor"></param>
        /// <returns></returns>
        public bool AddattInfor(Attendance atinfor)
        {
            if (atserver.AddattInfor(atinfor) >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 通过班级编号，获取考勤表
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public DataTable GetAttByCId(int cid)
        {
            return atserver.GetAttInByCId(cid);
        }

        /// <summary>
        /// 通过班级编号查看每个人出勤次数
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public List<AttInforExt> GetAttRateByCId(int cid)
        {
            return atserver.GetAttRateByCId(cid);
        }
    }
}
