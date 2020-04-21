using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moder
{
    [Serializable]
    public class SalesPerson
    {
        /// <summary>
        /// 销售员id
        /// </summary>
        public int SalesPersonId { get; set; }
        /// <summary>
        /// 销售员名
        /// </summary>
        public string SPName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string LoginPwd { get; set; }
        /// <summary>
        /// 职员登录
        /// </summary>
        public int LogId { get; set; }
    }
}
