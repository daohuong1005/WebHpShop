using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebHpShop.Reponsitory.Dto
{
    public class ManufacturerDto
    {
        public long manufacturerId { get; set; }
        [Required(ErrorMessage = "Không Được Để Trống")]
        public string manufacturerName { get; set; }
    }
}
