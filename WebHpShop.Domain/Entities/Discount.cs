using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebHpShop.Domain.Entities
{
    public class Discount : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long DiscountId { get; set; }
        public string DiscountContent { get; set; }
        public DateTime DiscountDateStart { get; set; }
        public DateTime DiscountDateEnd { get; set; }
        public double DiscountMoney { get; set; }
        [ForeignKey("ProductId")]
        public long ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
