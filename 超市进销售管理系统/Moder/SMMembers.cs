using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moder
{
    [Serializable]
    public class SMMembers
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public int Points { get; set; }
        public string PhoneNumber { get; set; }
        public string MemberAddress { get; set; }
        public DateTime OpenTime { get; set; }
        public int MemberStatus { get; set; }
    }
}
