using System;
using System.Collections.Generic;
using System.Text;

namespace WebHpShop.Service.ViewModel
{
    public class ProductDiscounViewModel
    {
        public long Id { get; set; }
        public string ProductName { get; set; }
        public double? Price { get; set; }
        public double? DisscountMoney { get; set; }
        public string Image { get; set; }
        
        public double? sale { get; set; } 
        public string ManufacturerName { get; set; }
    }
}
