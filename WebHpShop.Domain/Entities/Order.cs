using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebHpShop.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Order()
        {
            OrderDate = DateTime.Now;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long OrderId { get; set; }
        [Required]
        [ForeignKey("UserId")]
        public long UserId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public DateTime CloseDate { get; set; }
        public DateTime? ShipDate { get; set; }

        public string OrderAddress { get; set; }
        public double Total { get; set; }
        [ForeignKey("StatusId")]
        public int StatusId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual Status Status { get; set; }



    }
}
