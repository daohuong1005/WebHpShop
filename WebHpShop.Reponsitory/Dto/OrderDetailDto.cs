using System;
using System.Collections.Generic;
using System.Text;

namespace WebHpShop.Reponsitory.Dto
{
    public class OrderDetailDto
    {

        public long ProductId { get; set; }
        public long OrderId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
