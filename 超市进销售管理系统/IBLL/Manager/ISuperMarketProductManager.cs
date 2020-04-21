using Moder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL.Manager
{
    public interface ISuperMarketProductManager
    {
        List<ProductCategory> GetCategories();
        List<ProductUnit> GetUnit();

        bool InsertProduct(Produts produt, ProductInventory inventory);
        List<Produts> GetAllProduct();
        List<Produts> GetProductsWithWhere(int categoryId, string where);

        Produts GetProductWithId(string pid);
        bool InventoryProduct(string pid, int count);
        bool SetProductDiscount(string pid, float discount);

        bool UpdateProduct(Produts produts);
    }
}
