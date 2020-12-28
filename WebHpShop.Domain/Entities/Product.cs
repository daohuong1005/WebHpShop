using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebHpShop.Domain.Entities
{
    public class Product : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ProductId { get; set; }
        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; }
        [Required]
        [ForeignKey("SupplierId")]
        public long SupplierId { get; set; }
        [Required]
        [ForeignKey("CategoryId")]
        public long CategoryId { get; set; }
        [Required]
        [ForeignKey("ManufacturerId")]
        public long ManufacturerId { get; set; }
        [MaxLength(50)]
        public string OperatingSystem { get; set; }
        [MaxLength(50)]
        public string CPU { get; set; }
        [MaxLength(50)]
        public string GPU { get; set; }
        [MaxLength(50)]
        public string Ram { get; set; }
        public string Rom { get; set; }
        public string Screen { get; set; }
        [MaxLength(50)]
        public string Pixel { get; set; }
        public string Size { get; set; }
        [MaxLength(50)]
        public string CameraFirst { get; set; }
        public string CameraLast { get; set; }
        public string Support { get; set; }
        public string Description { get; set; }
        [Required]
        public double CostBuy { get; set; }
        [Required]
        public double CostSell { get; set; }

        [MaxLength(50)]
        public string NameColor { get; set; }
        [MaxLength(50)]
        public string ColorCode { get; set; }
        [Required]

        public int InStock { get; set; }

        public virtual Category Category { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public virtual ICollection<Discount> Discounts { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
