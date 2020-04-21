using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Manager;
using IBLL.Manager;
using IDAL.Manager;
using Moder;

namespace BLL.Manager
{
    public class SuperMarketLoginLogManager : ISuperMarketLoginLogManager
    {
        ISuperMarketLoginLogServer server = new SuperMarketLoginLogServer();

        public List<LoginLogs> GetLoginLog()
        {
            return server.GetLoginLog();
        }

        public List<LoginLogs> GetLoginLogBy(DateTime starttime, DateTime dateTime, string wheres, int check)
        {
            return server.GetLoginLogBy(starttime, dateTime, wheres, check);
        }
    }
}
