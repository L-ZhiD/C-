using DAL.Cashier;
using IBLL.Cashier;
using IDAL.Cashier;
using Moder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Cashier
{
    public class SuperMarketMemberManager : ISuperMarketMemberManager
    {
        ISuperMarketMemberServer server = new SuperMarketMemberServer();
        public SMMembers GetMembersById(string id)
        {
            return server.GetMembersById(id);
        }
    }
}
