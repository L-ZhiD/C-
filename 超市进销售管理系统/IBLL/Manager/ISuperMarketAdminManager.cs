using Moder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL.Manager
{
    public interface ISuperMarketAdminManager
    {
        SysAdmins AdminLogin(SysAdmins admin);
        bool AdminUpdatePwd(SysAdmins admin);
        List<SysAdmins> GetAdmins();
        bool SetSysStatus(SysAdmins admin);
        SysAdmins InsertAdmin(SysAdmins admin);
        SysAdmins UpdateAdmin(SysAdmins admin);
        List<SalesPerson> GetSales();
        SalesPerson InsertSales(SalesPerson person);
        SalesPerson UpdateSales(SalesPerson person);
    }
}
