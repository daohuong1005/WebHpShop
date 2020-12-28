using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebHpShop.Reponsitory.Dto
{
    public class SupplierDto
    {
       
        public long SupplierId { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage ="Không được bỏ trống")]
        public string SupplierName { get; set; }
        [MaxLength(10)]
        [Required(ErrorMessage ="Không được bỏ trống")]
        public string SupplierCode { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
    }
}
