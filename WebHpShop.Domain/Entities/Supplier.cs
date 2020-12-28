using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebHpShop.Domain.Entities
{
    public class Supplier : BaseEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SupplierId { get; set; }
        [MaxLength(100)]
        [Required]
        public string SupplierName { get; set; }
        [MaxLength(10)]
        [Required]
        public string SupplierCode { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
