using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moder
{
    [Serializable]
    public class ProductInventory
    {
        public string ProductId { get; set; }
        public int TotalCount { get; set; }
        public int MinCount { get; set; }
        public int MaxCount { get; set; }
        public int StatusId { get; set; }

        public string StatusDesc { get; set; }
    }
}
