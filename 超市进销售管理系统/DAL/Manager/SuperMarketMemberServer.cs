using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.Manager;
using Moder;

namespace DAL.Manager
{
    public class SuperMarketMemberServer : ISuperMarketMemberServer
    {
        public SMMembers AddMember(SMMembers member)
        {
            string procName = "AddMember";
            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("@memberName",member.MemberName),
                new SqlParameter("@phoneNumber",member.PhoneNumber),
                new SqlParameter("@memberAddress", member.MemberAddress)
            };
            object res = SQLHelper.ExecuteScalar(procName, sp);
            if (res != null)
            {
                member.MemberId = Convert.ToInt32(res);
            }
            else
            {
                member = null;
            }
            return member;
        }

        public SMMembers GetMemberByIdOrPhone(string id)
        {
            Cashier.SuperMarketMemberServer server = new Cashier.SuperMarketMemberServer();
            return server.GetMembersById(id);
        }
    }
}
