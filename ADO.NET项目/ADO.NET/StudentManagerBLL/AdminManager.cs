using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentManagerModel;
using StudentManagerDAL;

namespace StudentManagerBLL
{
    public class AdminManager
    {
        AdminServer server = new AdminServer();
        public Admins GetAdmins(Admins adm)
        {
            return server.GetAdmins(adm);
        }
        public int AddAdmins(Admins adm)
        {
            return server.ADDAdmins(adm);
        }
    }
}
