using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebHpShop.Domain.Entities
{
    public class Comment : BaseEntity
    {
       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CommentId { get; set; }
        public string Content { get; set; }
        [Required]
        [ForeignKey("ProductId")]
        public long ProductId { get; set; }
        [Required]
        [ForeignKey("UserId")]
        public long UserId { get; set; }

        public virtual Product Product { get; set; }
        public virtual User User { get; set; }

    }
}
