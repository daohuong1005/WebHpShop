using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebHpShop.Reponsitory.Dto
{
    public class ProductDto
    {
        public long ProductId { get; set; }
        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; }
        [Required]
        public long SupplierId { get; set; }
        [Required]
        public long CategoryId { get; set; }
        [Required]
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

    }
}
