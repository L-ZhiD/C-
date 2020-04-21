
using Moder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.Manager;
using IBLL.Manager;
using DAL.Manager;

namespace BLL.Manager
{
    public class SuperMarketProductManager : ISuperMarketProductManager
    {
        ISuperMarketProductServer product = new SuperMarketProductServer();

        public List<Produts> GetAllProduct()
        {
            return product.GetAllProduct();
        }

        public List<ProductCategory> GetCategories()
        {
            return product.GetCategories();
        }

        public List<Produts> GetProductsWithWhere(int categoryId, string where)
        {
            return product.GetProductsWithWhere(categoryId, where);
        }

        public Produts GetProductWithId(string pid)
        {
            return product.GetProductWithId(pid);
        }

        public List<ProductUnit> GetUnit()
        {
            return product.GetUnit();
        }

        public bool InsertProduct(Produts produt, ProductInventory inventory)
        {
            return product.InsertProduct(produt, inventory);
        }

        public bool InventoryProduct(string pid, int count)
        {
            if (product.InventoryProduct(pid, count) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SetProductDiscount(string pid, float discount)
        {
            if (product.SetProductDiscount(pid, discount) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateProduct(Produts produts)
        {
            if (product.UpdateProduct(produts) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
