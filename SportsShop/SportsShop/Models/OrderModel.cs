using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsShop.Models
{
    public class OrderModel
    {
        public int OrderNumber { get; set; }
        public string Order_Date { get; set; }
        public string ListOfItem { get; set; }
        public string PaymentMode { get; set; }
        public string OrderDeliveryDate { get; set; }
        public int CustomerNumber { get; set; }

    }
}
