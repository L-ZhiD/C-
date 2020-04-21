using IDAL.Manager;
using Moder;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Manager
{
    public class SuperMarketAdminServer : ISuperMarketAdminServer
    {
        public SysAdmins AdminLogin(SysAdmins admin)
        {
            string procName = "SysAdminLogin";
            SqlParameter[] sp = new SqlParameter[] {
                    new SqlParameter("@logId",admin.LoginId),
                    new SqlParameter("@logPwd",admin.LoginPwd)
            };
            SqlDataReader reader = SQLHelper.GetDataReader(procName, sp);
            SysAdmins admins = null;
            while (reader.Read())
            {
                admins = new SysAdmins()
                {
                    AdminName = reader["AdminName"].ToString(),
                    LoginId = Convert.ToInt32(reader["LoginId"]),
                    LoginPwd = reader["LoginPwd"].ToString(),
                    Roleld = Convert.ToInt32(reader["Roleld"]),
                    AdminStatus = Convert.ToInt32(reader["AdminStatus"])
                };
            }
            reader.Close();
            return admins;
        }

        public int AdminUpdatePwd(SysAdmins admin)
        {
            string procName = "UpdateSysPwd";
            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("@logId",admin.LoginId),
                new SqlParameter("@logPwd",admin.LoginPwd)
            };
            return SQLHelper.ExecuteNonQuery(procName, sp);
        }
        public List<SysAdmins> GetAdmins()
        {
            string procName = "GetAllTables";
            SqlParameter[] sp =
            {
                new SqlParameter("@tableName","SysAdmins")
            };
            SqlDataReader reader = SQLHelper.GetDataReader(procName, sp);
            List<SysAdmins> list = new List<SysAdmins>();
            while (reader.Read())
            {
                SysAdmins admins = new SysAdmins();
                admins.AdminName = reader["AdminName"].ToString();
                admins.LoginId = Convert.ToInt32(reader["LoginId"]);
                admins.LoginPwd = reader["LoginPwd"].ToString();
                admins.Roleld = Convert.ToInt32(reader["RoleId"]);
                admins.AdminStatus = Convert.ToInt32(reader["AdminStatus"]);
                admins.StatusName = admins.AdminStatus == 1 ? "启用" : "禁用";
                admins.RoleName = admins.Roleld == 1 ? "超级管理员" : "一般管理员";
                list.Add(admins);
            }
            reader.Close();
            return list;
        }

        public List<SalesPerson> GetSales()
        {
            string procName = "GetAllTables";
            SqlParameter[] sp =
            {
                new SqlParameter("@tableName","SalesPerson")
            };
            SqlDataReader reader = SQLHelper.GetDataReader(procName, sp);
            List<SalesPerson> list = new List<SalesPerson>();
            while (reader.Read())
            {
                SalesPerson admins = new SalesPerson();
                admins.SalesPersonId = Convert.ToInt32(reader["SalesPersonId"]);
                admins.LoginPwd = reader["LoginPwd"].ToString();
                admins.SPName = reader["SPName"].ToString();
                list.Add(admins);
            }
            reader.Close();
            return list;
        }

        public SysAdmins InsertAdmin(SysAdmins admin)
        {
            string procName = "InsertAdmin";
            SqlParameter[] sp =
            {
                new SqlParameter("@adminName",admin.AdminName),
                new SqlParameter("@loginPwd",admin.LoginPwd),
                new SqlParameter("@roleId",admin .Roleld)
            };
            object res = SQLHelper.ExecuteScalar(procName, sp);
            if (res != null)
            {
                admin.LoginId = Convert.ToInt32(res);
            }
            else
            {
                admin = null;
            }
            return admin;
        }

        public SalesPerson InsertSales(SalesPerson person)
        {
            string procName = "InsertSales";
            SqlParameter[] sp ={
                new SqlParameter("@spName",person.SPName),
                new SqlParameter("@loginPwd",person.LoginPwd)
            };
            object res = SQLHelper.ExecuteScalar(procName, sp);
            if (res != null)
            {
                person.SalesPersonId = Convert.ToInt32(res);
            }
            else
            {
                person = null;
            }
            return person;
        }

        public int SetSysStatus(SysAdmins admin)
        {
            string procName = "SetSysAdmStatus";
            SqlParameter[] sp =
            {
                new SqlParameter("@role",admin.AdminStatus),
                new SqlParameter("@id",admin.LoginId)
            };
            return SQLHelper.ExecuteNonQuery(procName, sp);
        }

        public int UpdateAdmin(SysAdmins admin)
        {
            string procName = "UpdateAdmin";
            SqlParameter[] sp =
            {
                new SqlParameter("@adminName",admin.AdminName),
                new SqlParameter("@loginPwd",admin.LoginPwd),
                new SqlParameter("@roleId",admin.Roleld),
                new SqlParameter("@loginId",admin.LoginId)
            };
            return SQLHelper.ExecuteNonQuery(procName, sp);
        }

        public int UpdateSales(SalesPerson person)
        {
            string procName = "UpdateSaleInfor";
            SqlParameter[] sp =
            {
                new SqlParameter("@saleName",person.SPName),
                new SqlParameter("@loginPwd",person.LoginPwd),
                new SqlParameter("@loginId",person.SalesPersonId)
            };
            return SQLHelper.ExecuteNonQuery(procName, sp);
        }
    }
}
