using System;
using System.Collections.Generic;
using System.Text;

namespace WebHpShop.Service.ViewModel
{
    public class OrderDetailViewModel
    {
        public long OrderId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public long ProductId { get; set; }
        public string Img { get; set; }
    }
}
