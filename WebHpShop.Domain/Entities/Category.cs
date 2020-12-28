using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebHpShop.Domain.Entities
{
    public class Category : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CategoryId { get; set; }
        [MaxLength(100)]
        [Required]
        public string CatergoryName { get; set; }
        public virtual ICollection<Product> Products { get; set; }

    }
}
