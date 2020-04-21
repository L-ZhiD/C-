using Moder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL.Cashier
{
    public interface ISuperMarketSaleServer
    {
        /// <summary>
        /// 收银员登录信息
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        SalesPerson SalesLogin(SalesPerson person);
        /// <summary>
        /// 记录到日志
        /// </summary>
        /// <param name="logs"></param>
        /// <returns></returns>
        int WriteSalesLog(LoginLogs logs);
        /// <summary>
        /// 受影响行数
        /// </summary>
        /// <param name="logid"></param>
        /// <returns></returns>
        int WriteSalesExitLog(int logid);

        DateTime GetSysTime();

        Produts GetProductWithId(string productId);
    }
}
