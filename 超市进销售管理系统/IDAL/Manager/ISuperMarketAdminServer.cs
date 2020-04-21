using Moder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL.Manager
{
    public interface ISuperMarketAdminServer
    {
        SysAdmins AdminLogin(SysAdmins admin);

        int AdminUpdatePwd(SysAdmins admin);

        List<SysAdmins> GetAdmins();

        int SetSysStatus(SysAdmins admin);

        SysAdmins InsertAdmin(SysAdmins admi);

        int UpdateAdmin(SysAdmins admin);

        List<SalesPerson> GetSales();

        SalesPerson InsertSales(SalesPerson person);

        int UpdateSales(SalesPerson person);
    }
}
