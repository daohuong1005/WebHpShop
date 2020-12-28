using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebHpShop.Domain.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreateDate = DateTime.Now;   
        }
        public DateTime CreateDate { get; set; } 
        [MaxLength(100)]
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; } 
        [MaxLength(100)]
        public string UpdateBy { get; set; }
        [Required]
        public bool IsDelete { get; set; } = false;
        [Required]
        public bool IsDisplay { get; set; } = true;

    }
}
