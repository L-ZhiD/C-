using Moder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL.Cashier
{
    /// <summary>
    /// 商品服务
    /// </summary>
    public interface ISuperMarketProductServer
    {
        Produts GetProductWithId(string productId);
        //启动事务处理购买商品对象更新会员积分
        bool SaveSalerInfo(SalesList sales, SMMembers members);
    }
}
