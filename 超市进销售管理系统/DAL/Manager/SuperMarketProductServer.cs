using IDAL.Cashier;
using Moder;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Manager
{
    public class SuperMarketProductServer : IDAL.Manager.ISuperMarketProductServer
    {
        public List<Produts> GetAllProduct()
        {
            string procName = "GetAllProduct";
            List<Produts> list = new List<Produts>();
            SqlDataReader reader = SQLHelper.GetDataReader(procName, null);
            while (reader.Read())
            {
                list.Add(new Produts()
                {
                    ProductId = reader["ProductId"].ToString(),
                    ProductName = reader["ProductName"].ToString(),
                    CategoryId = Convert.ToInt32(reader["CategoryId"]),
                    Discount = Convert.ToSingle(reader["Discount"]),
                    Unit = reader["Unit"].ToString(),
                    UnitPrice = Convert.ToDecimal(reader["UnitPrice"])
                });
            }
            reader.Close();
            return list;
        }

        public List<ProductCategory> GetCategories()
        {
            string procName = "GetProductCategory";
            List<ProductCategory> list = new List<ProductCategory>();
            SqlDataReader reader = SQLHelper.GetDataReader(procName, null);
            while (reader.Read())
            {
                list.Add(new ProductCategory()
                {
                    CategoryId = Convert.ToInt32(reader["CategoryId"]),
                    CategoryName = reader["CategoryName"].ToString()
                });
            }
            reader.Close();
            return list;
        }

        public List<Produts> GetProductsWithWhere(int categoryId, string where)
        {
            string procName = "GetProductsByWhere";
            SqlParameter[] sp =
            {
                new SqlParameter("@category",categoryId),
                new SqlParameter("@where",where)
            };
            List<Produts> list = new List<Produts>();
            SqlDataReader reader = SQLHelper.GetDataReader(procName, sp);
            while (reader.Read())
            {
                list.Add(new Produts()
                {
                    ProductId = reader["ProductId"].ToString(),
                    ProductName = reader["ProductName"].ToString(),
                    CategoryId = Convert.ToInt32(reader["CategoryId"]),
                    Discount = Convert.ToSingle(reader["Discount"]),
                    Unit = reader["Unit"].ToString(),
                    UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                    CategoryName = reader["CategoryName"].ToString(),
                    TotalCount = Convert.ToInt32(reader["TotalCount"])
                });
            }
            reader.Close();
            return list;

        }

        public Produts GetProductWithId(string pid)
        {
            string procName = "GetProductWithId";
            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("@productId",pid)
            };
            Produts produt = null;
            SqlDataReader reader = SQLHelper.GetDataReader(procName, sp);
            while (reader.Read())
            {
                produt = new Produts()
                {
                    ProductId = reader["ProductId"].ToString(),
                    ProductName = reader["ProductName"].ToString(),
                    Discount = Convert.ToSingle(reader["Discount"]),
                    CategoryId = Convert.ToInt32(reader["CategoryId"]),
                    CategoryName = reader["CategoryName"].ToString(),
                    Unit = reader["Unit"].ToString(),
                    UnitPrice = Convert.ToDecimal(reader["UnitPrice"])
                };
            }
            reader.Close();
            return produt;
        }

        public List<ProductUnit> GetUnit()
        {
            string procName = "GetProductUnit";
            List<ProductUnit> list = new List<ProductUnit>();
            SqlDataReader reader = SQLHelper.GetDataReader(procName, null);
            while (reader.Read())
            {
                list.Add(new ProductUnit()
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Unit = reader["Unit"].ToString()
                });
            }
            reader.Close();
            return list;
        }

        public bool InsertProduct(Produts produt, ProductInventory inventory)
        {
            List<string> procList = new List<string>()
            {
                "InsertProduct",
                "InsertInventory"
            };

            List<SqlParameter[]> psList = new List<SqlParameter[]>();
            SqlParameter[] prodPs = new SqlParameter[]
            {
                new SqlParameter("@productId",produt.ProductId),
                new SqlParameter("@productName",produt.ProductName),
                new SqlParameter("@unitPrice",produt.UnitPrice),
                new SqlParameter("@unit",produt.Unit),
                new SqlParameter("@discount",produt.Discount),
                new SqlParameter("@categoryId",produt.CategoryId)
            };

            SqlParameter[] inventPs = new SqlParameter[]
            {
                new SqlParameter("@productId",inventory.ProductId),
                new SqlParameter("@minCount",inventory.MinCount),
                new SqlParameter("@maxCount",inventory.MaxCount)
            };
            psList.Add(prodPs);
            psList.Add(inventPs);
            return SQLHelper.UpdateByTran(procList, psList);
        }

        public int InventoryProduct(string pid, int count)
        {
            string procName = "InventoryIn";
            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("@productId",pid),
                new SqlParameter("@count",count)
            };
            return SQLHelper.ExecuteNonQuery(procName, sp);
        }

        public int SetProductDiscount(string pid, float discount)
        {
            string procName = "SetProductDiscount";
            SqlParameter[] sp =
            {
                new SqlParameter("@discount",discount),
                new SqlParameter("@productId",pid)
            };
            return SQLHelper.ExecuteNonQuery(procName, sp);
        }

        public int UpdateProduct(Produts produts)
        {
            string procName = "SetProductInfor";
            SqlParameter[] sp =
            {
                new SqlParameter("@productName",produts.ProductName),
                new SqlParameter("@unitPrice",produts.UnitPrice),
                new SqlParameter("@categoryId",produts.CategoryId),
                new SqlParameter("@unit",produts.Unit),
                new SqlParameter("@productId",produts.ProductId)
            };
            return SQLHelper.ExecuteNonQuery(procName, sp);
        }
    }
}
