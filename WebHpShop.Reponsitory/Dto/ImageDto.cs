using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebHpShop.Reponsitory.Dto
{
    public class ImageDto
    {
        public long ImageId { get; set; }
        [Required]
        public IFormFile PathImage { get; set; }
        public string Url { get; set; }
        [Required]
        public long ProductId { get; set; }
        [Required]
        public bool IsDisplay { get; set; } = true;
        [Required]
        public bool IsDelete { get; set; } = false;

    }
}
