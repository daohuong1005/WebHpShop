using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebHpShop.Domain.Entities
{
    public class Role 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RoleId { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
