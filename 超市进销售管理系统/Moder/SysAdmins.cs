using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moder
{
    public class SysAdmins
    {
        public int LoginId { get; set; }
        public string LoginPwd { get; set; }
        public string AdminName { get; set; }
        public int AdminStatus { get; set; }
        public int Roleld { get; set; }

        //扩展属性
        public string StatusName { get; set; }
        public string RoleName { get; set; }

        //管理员登录和退出时间也需要记录
        public int LoginLogId { get; set; }
    }
}
