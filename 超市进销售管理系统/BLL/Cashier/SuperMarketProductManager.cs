using DAL.Cashier;
using IBLL.Cashier;
using IDAL.Cashier;
using Moder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Cashier
{
    public class SuperMarketProductManager : ISuperMarketProductManager
    {
        ISuperMarketProductServer server = new SuperMarketProductServer();
        public Produts GetProductWithId(string productId)
        {
            return server.GetProductWithId(productId);
        }

        public bool SaveSalerInfo(SalesList sales, SMMembers members)
        {
            return server.SaveSalerInfo(sales, members);
        }
    }
}
