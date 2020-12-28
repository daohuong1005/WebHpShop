using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebHpShop.Reponsitory.Dto
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Không được để trống")]
        [StringLength(maximumLength: 50, MinimumLength = 5, ErrorMessage = "Tên tài khoản trên 5 kí tự")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [StringLength(maximumLength: 50, MinimumLength = 5, ErrorMessage = "Mật Khẩu phải trên 5 kí tự")]
        public string Password { get; set; }
    }
}
