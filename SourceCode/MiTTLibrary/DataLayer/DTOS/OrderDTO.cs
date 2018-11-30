using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiTTLibrary
{
    public class OrderDTO
    {
        public string OrderId { get; set; }
        public string UserId { get; set; }
        public string Note { get; set; }
        public DateTime BuyDate { get; set; }
        public bool StatusOrder { get; set; }
    }
}
