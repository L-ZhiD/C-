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
    public class ScoreManager
    {
        ScoreServer scoserver = new ScoreServer();//处理成绩数据
        /// <summary>
        /// 通过班级名, 获取成绩表数据
        /// </summary>
        public List<StuCoreExt> GetScore(int cid)
        {
            return scoserver.GetScore(cid);
        }
        /// <summary>
        /// 通过学号获取成绩表数据
        /// </summary>
        /// <param name="id">学号</param>
        /// <returns></returns>
        public StuCoreExt GetStudentById(int id)
        {
            return scoserver.GetScoreById(id);
        }

        /// <summary>
        /// 通过学号或姓名(模糊查询)
        /// </summary>
        /// <param name="target">学号或姓名</param>
        /// <returns></returns>
        public List<StuCoreExt> GetStuScoreIDorName(string target)
        {
            return scoserver.GetStuscoreExts(target);
        }
        /// <summary>
        /// 修改学员成绩
        /// </summary>
        /// <param name="score">学员对象</param>
        /// <returns></returns>
        public bool UpScore(StuCoreExt score)
        {
            if (scoserver.UpScore(score) <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 删除学员成绩
        /// </summary>
        /// <param name="sco"></param>
        /// <returns></returns>
        public bool DeleScore(int id)
        {
            if (scoserver.DeleScore(id) <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 添加学员
        /// </summary>
        /// <param name="sco"></param>
        /// <returns></returns>
        public bool AddScore(StuCoreExt sco)
        {
            int addsc = scoserver.AddScore(sco);
            if (addsc <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 通过班级编辑号，获取对应成绩表
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public DataTable GetScoreByCId(int cid)
        {
            return scoserver.GetScoreByCId(cid);
        }
    }
}
