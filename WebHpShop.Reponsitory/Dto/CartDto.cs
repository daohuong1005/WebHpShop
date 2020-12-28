using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebHpShop.Reponsitory.Dto
{
    public class CartDto
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string ProducName { get; set; }
        public int Quantity { get; set; }
        public string PathImage { get; set; }
        public double Price { get; set; }
        public DateTime? Date { get; set; }
    }
}
