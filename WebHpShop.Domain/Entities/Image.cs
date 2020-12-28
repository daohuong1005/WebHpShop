using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebHpShop.Domain.Entities
{
    public class Image
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ImageId { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        [ForeignKey("ProductId")]
        public long ProductId { get; set; }
        [Required]
        public bool IsDisplay { get; set; }
        [Required]
        public bool IsDelete { get; set; }

        public virtual Product Product { get; set; }
    }
}
