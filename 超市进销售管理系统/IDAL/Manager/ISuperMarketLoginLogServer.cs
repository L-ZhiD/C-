using Moder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL.Manager
{
    public interface ISuperMarketLoginLogServer
    {
        List<LoginLogs> GetLoginLog();

        List<LoginLogs> GetLoginLogBy(DateTime starttime, DateTime dateTime, string wheres, int check);
    }
}
