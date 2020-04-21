using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBLL;
using IDAL;
using DAL;
using Moder;

namespace BLL.Cashier
{
    public class SaleManager:IBLL.Cashier.ISuperMarketSaleManager
    {
        IDAL.Cashier.ISuperMarketSaleServer saleServer = new DAL.Cashier.SuperMarketSaleServer();
        /// <summary>
        /// 日志
        /// </summary>
        /// <param name="loginLogs"></param>
        /// <returns></returns>
        public int BllWriteSelesLog(LoginLogs loginLogs)
        {
            return saleServer.WriteSalesLog(loginLogs);
        }

        public Produts GetProductWithId(string productId)
        {
            return saleServer.GetProductWithId(productId);
        }

        public DateTime GetSysTime()
        {
            return saleServer.GetSysTime();
        }

        public SalesPerson SalesLogin(SalesPerson person)
        {
            return saleServer.SalesLogin(person);
        }

        public bool WriteSalesExitLog(int logid)
        {
            if (saleServer.WriteSalesExitLog(logid) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int WriteSalesLog(LoginLogs logs)
        {
            return saleServer.WriteSalesLog(logs);
        }
    }
}
