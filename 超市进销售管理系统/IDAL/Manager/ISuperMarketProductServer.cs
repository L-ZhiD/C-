using Moder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL.Manager
{
    public interface ISuperMarketProductServer
    {
        List<ProductCategory> GetCategories();
        List<ProductUnit> GetUnit();
        bool InsertProduct(Produts produt, ProductInventory inventory);

        List<Produts> GetAllProduct();

        Produts GetProductWithId(string pid);

        int InventoryProduct(string pid, int count);

        List<Produts> GetProductsWithWhere(int categoryId, string where);

        int SetProductDiscount(string pid, float discount);

        int UpdateProduct(Produts produts);
    }
}
