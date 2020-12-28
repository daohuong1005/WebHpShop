using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebHpShop.Reponsitory.Dto
{
    public class CategoryDto
    {
        public long CategoryId { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "Không được để trống")]
        public string CatergoryName { get; set; }
        [Required]
        public long ManufacturerId { get; set; }
    }
}
