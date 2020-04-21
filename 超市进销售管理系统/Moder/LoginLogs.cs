using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moder
{
    [Serializable]
    public class LoginLogs
    {
        public int LogId { get; set; }
        /// <summary>
        /// 登陆者id
        /// </summary>
        public int LoginId { get; set; }
        /// <summary>
        /// 登陆人姓名
        /// </summary>
        public string SPName { get; set; }
        /// <summary>
        /// 登录服务器名称
        /// </summary>
        public string ServerName { get; set; }
        /// <summary>
        /// 登陆时间
        /// </summary>
        public DateTime LoginTime { get; set; }
        /// <summary>
        /// 退出时间
        /// </summary>
        public DateTime? ExitTime { get; set; }
    }
}
