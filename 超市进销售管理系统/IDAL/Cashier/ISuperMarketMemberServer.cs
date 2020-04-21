using Moder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL.Cashier
{
    public interface ISuperMarketMemberServer
    {
        SMMembers GetMembersById(string id);
    }
}
