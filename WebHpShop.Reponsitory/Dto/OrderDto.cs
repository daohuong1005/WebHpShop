using System;
using System.Collections.Generic;
using System.Text;

namespace WebHpShop.Reponsitory.Dto
{
    public class OrderDto
    {
        public long OrderId { get; set; }

        public long UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime CloseDate { get; set; }
        public double Freight { get; set; }
        public DateTime? ShipDate { get; set; }
        public double Total { get; set; }
    }
}
