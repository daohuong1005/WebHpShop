using System;
using System.Collections.Generic;
using System.Text;

namespace WebHpShop.Service.ViewModel
{
    public class ProductViewModel
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public long SupplierId { get; set; }
        public long CategoryId { get; set; }
        public long ManufacturerId { get; set; }
        public string OperatingSystem { get; set; }
        public string CPU { get; set; }
        public string GPU { get; set; }
        public string Ram { get; set; }
        public string Rom { get; set; }
        public string Screen { get; set; }
        public string Pixel { get; set; }
        public string Size { get; set; }
        public string CameraFirst { get; set; }
        public string CameraLast { get; set; }
        public string Support { get; set; }
        public string Description { get; set; }
        public double CostBuy { get; set; }
        public double CostSell { get; set; }
        public string Color { get; set; }
        public int InStock { get; set; }

    }
}
