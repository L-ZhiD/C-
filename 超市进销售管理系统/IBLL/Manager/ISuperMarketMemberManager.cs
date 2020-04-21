using Moder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL.Manager
{
    public interface ISuperMarketMemberManager
    {
        SMMembers GetMemberByIdOrPhone(string id);
        SMMembers AddMember(SMMembers member);
    }
}
