using IDAL.Cashier;
using Moder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Cashier
{
    public class SuperMarketProductServer : ISuperMarketProductServer
    {
        public Produts GetProductWithId(string productId)
        {
            string procName = "GetProductWithId";
            SqlParameter[] sp =
            {
                new SqlParameter("@productId", SqlDbType.NVarChar,50)
            };
            sp[0].Value = productId;
            SqlDataReader reader = SQLHelper.GetDataReader(procName, sp);
            Produts produt = null;
            while (reader.Read())
            {
                produt = new Produts()
                {
                    ProductId = reader["ProductId"].ToString(),
                    ProductName = reader["ProductName"].ToString(),
                    UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                    Unit = reader["Unit"].ToString(),
                    Discount = Convert.ToDouble(reader["Discount"]),
                    CategoryId = Convert.ToInt32(reader["CategoryId"]),
                    CategoryName = reader["CategoryName"].ToString()
                };
            }
            reader.Close();
            return produt;
        }

        public bool SaveSalerInfo(SalesList sales, SMMembers members)
        {
            List<string> procList = new List<string>();
            List<SqlParameter[]> psList = new List<SqlParameter[]>();
            //给消费主表中添加数据
            procList.Add("AddSalesList");
            SqlParameter[] saleSp = new SqlParameter[] {
                new SqlParameter("@SerialNum", SqlDbType.NVarChar,50),
                new SqlParameter("@TotalMoney", SqlDbType.Decimal),
                new SqlParameter("@RealReceive", SqlDbType.Decimal),
                new SqlParameter("@ReturnMoney", SqlDbType.Decimal),
                new SqlParameter("@SalesPersonId", SqlDbType.Int)
            };
            saleSp[0].Value = sales.SerialNum;
            saleSp[1].Value = sales.TotalMoney;
            saleSp[2].Value = sales.RealReceive;
            saleSp[3].Value = sales.ReturnMoney;
            saleSp[4].Value = sales.SalesPersonId;
            psList.Add(saleSp);
            //给消费明细表中添加每次购物的详细数据
            foreach (SalesListDetail detail in sales.ListDetail)
            {
                procList.Add("AddSalesListDetail");
                SqlParameter[] detailList = new SqlParameter[] {
                  new SqlParameter("@SerialNum", SqlDbType.NVarChar,50),
                  new SqlParameter("@ProductId", SqlDbType.NVarChar,50),
                  new SqlParameter("@ProductName", SqlDbType.NVarChar,50),
                  new SqlParameter("@UnitPrice", SqlDbType.Decimal),
                  new SqlParameter("@Discount", SqlDbType.Float),
                  new SqlParameter("@Quantity", SqlDbType.Int),
                  new SqlParameter("@SubTotalMoney", SqlDbType.Decimal)
                };
                detailList[0].Value = detail.SerialNum;
                detailList[1].Value = detail.ProductId;
                detailList[2].Value = detail.ProductName;
                detailList[3].Value = detail.UnitPrice;
                detailList[4].Value = detail.Discount;
                detailList[5].Value = detail.Quantity;
                detailList[6].Value = detail.SubTotalMoney;
                psList.Add(detailList);
            }
            //更新会员的积分
            if (members != null)
            {
                procList.Add("RefreshMemberPoint");
                SqlParameter[] memberSp = new SqlParameter[]
                {
                new SqlParameter("@point", SqlDbType.Int),
                new SqlParameter("@memberId", SqlDbType.Int)
                };
                memberSp[0].Value = members.Points;
                memberSp[1].Value = members.MemberId;
                psList.Add(memberSp);
            }
            return SQLHelper.UpdateByTran(procList, psList);
        }
    }
}
