using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebHpShop.Domain.Entities
{
    public class OrderDetail : BaseEntity
    {


        [Key, Column(Order = 1)]
        [ForeignKey("ProductId")]
        public long ProductId { get; set; }
        [Key, Column(Order = 2)]
        [ForeignKey("OrderId")]
        public long OrderId { get; set; }
        [Required]
        public int Quantity { get; set; }
        public double Price { get; set; }

        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }


    }
}
