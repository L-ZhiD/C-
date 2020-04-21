using Moder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL.Cashier
{
    public interface ISuperMarketSaleManager
    {
        /// <summary>
        /// 登录人员
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        SalesPerson SalesLogin(SalesPerson person);
        int WriteSalesLog(LoginLogs logs);
        bool WriteSalesExitLog(int logid);
        DateTime GetSysTime();
        Produts GetProductWithId(string productId);
        /// <summary>
        /// 记录到日志信息
        /// </summary>
        /// <param name="loginLogs"></param>
        /// <returns></returns>
        int BllWriteSelesLog(LoginLogs loginLogs);
    }
}
