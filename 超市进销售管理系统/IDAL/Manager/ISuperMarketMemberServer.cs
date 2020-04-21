using Moder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL.Manager
{
    public interface ISuperMarketMemberServer
    {
        SMMembers GetMemberByIdOrPhone(string id);
        SMMembers AddMember(SMMembers member);
    }
}
