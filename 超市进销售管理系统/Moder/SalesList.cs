using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moder
{
    /// <summary>
    /// 本次消费总计
    /// </summary>
    [Serializable]
    public class SalesList
    {
        public SalesList()
        {
            ListDetail = new List<SalesListDetail>();
        }
        public string SerialNum { get; set; }
        public decimal TotalMoney { get; set; }
        public decimal RealReceive { get; set; }
        public decimal ReturnMoney { get; set; }
        public int SalesPersonId { get; set; }
        public DateTime SaleDate { get; set; }
        //两个对象属于1对多的关系
        public List<SalesListDetail> ListDetail { get; set; }
    }
}
