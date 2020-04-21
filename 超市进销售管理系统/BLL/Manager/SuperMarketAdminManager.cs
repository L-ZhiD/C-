using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Cashier;
using DAL.Manager;
using IBLL.Manager;
using IDAL.Cashier;
using IDAL.Manager;
using Moder;

namespace BLL.Manager
{
    public class SuperMarketAdminManager : ISuperMarketAdminManager
    {
        ISuperMarketAdminServer adminServer = new SuperMarketAdminServer();
        ISuperMarketSaleServer saleServer = new SuperMarketSaleServer();
        public SysAdmins AdminLogin(SysAdmins admin)
        {
            //【1】根据用户账号和密码调用查询用户登录
            SysAdmins sys = adminServer.AdminLogin(admin);
            //管理员登录然后状态是启用，可以登录
            if (sys != null && sys.AdminStatus == 1)
            {
                //【2】写入登录日志
                LoginLogs log = new LoginLogs()
                {
                    LoginId = sys.LoginId,
                    SPName = sys.AdminName,
                    ServerName = Dns.GetHostName()
                };
                //保存当前管理员登录日志的ID
                sys.LoginLogId = saleServer.WriteSalesLog(log);
            }
            else
            {
                sys = null;
            }
            return sys;
        }

        public bool AdminUpdatePwd(SysAdmins admin)
        {
            int res = adminServer.AdminUpdatePwd(admin);
            if (res > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<SysAdmins> GetAdmins()
        {
            return adminServer.GetAdmins();
        }

        public List<SalesPerson> GetSales()
        {
            return adminServer.GetSales();
        }

        public SysAdmins InsertAdmin(SysAdmins admin)
        {
            return adminServer.InsertAdmin(admin);
        }

        public SalesPerson InsertSales(SalesPerson person)
        {
            return adminServer.InsertSales(person);
        }

        public bool SetSysStatus(SysAdmins admin)
        {
            if (adminServer.SetSysStatus(admin) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public SysAdmins UpdateAdmin(SysAdmins admin)
        {
            if (adminServer.UpdateAdmin(admin) > 0)
            {
                return admin;
            }
            else
            {
                return null;
            }
        }

        public SalesPerson UpdateSales(SalesPerson person)
        {
            if (adminServer.UpdateSales(person) > 0)
            {
                return person;
            }
            else
            {
                return null;
            }
        }
    }
}
