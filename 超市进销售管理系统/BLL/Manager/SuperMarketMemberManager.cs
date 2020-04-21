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
    public class SuperMarketMemberManager : ISuperMarketMemberManager
    {
        ISuperMarketMemberServer memberServer = new SuperMarketMemberServer();
        public SMMembers AddMember(SMMembers member)
        {
            return memberServer.AddMember(member);
        }

        public SMMembers GetMemberByIdOrPhone(string id)
        {
            return memberServer.GetMemberByIdOrPhone(id);
        }
    }
}
