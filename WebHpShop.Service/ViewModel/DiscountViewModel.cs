using System;
using System.Collections.Generic;
using System.Text;

namespace WebHpShop.Service.ViewModel
{
    public class DiscountViewModel
    {
        public long DiscountId { get; set; }
        public string DiscountContent { get; set; }
        public DateTime DiscountDateStart { get; set; }
        public DateTime DiscountDateEnd { get; set; }
        public double DiscountMoney { get; set; }
        public long ProductId { get; set; }
    }
}
