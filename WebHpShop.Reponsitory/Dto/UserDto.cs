using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebHpShop.Reponsitory.Dto
{
    public class UserDto
    {
      
        public long UserId { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [MaxLength(50,ErrorMessage = "Không được quá 50 kí tự")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [MaxLength(50, ErrorMessage = "Không được quá 50 kí tự")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [StringLength(maximumLength:50,MinimumLength = 5, ErrorMessage = "Tên tài khoản phải trên 5 kí tự")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [StringLength(maximumLength: 50, MinimumLength = 5, ErrorMessage = "Mật Khẩu phải trên 5 kí tự ")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        public DateTime Birthday { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        public bool Gender { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [EmailAddress(ErrorMessage = "Xin nhập đúng định dạng Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [MaxLength(11)]
        public string Phone { get; set; }
        public string OrderAddress { get; set; }
        public string ImgAvatar { get; set; } = "0000userPng.png";
        public IFormFile ImageAvt { get; set; }
        public bool IsActive { get; set; } = true;
        [Required]
        public long RoleId { get; set; }
    }
}
