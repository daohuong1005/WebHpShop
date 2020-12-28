using System;
using System.Collections.Generic;
using System.Text;

namespace WebHpShop.Service.ViewModel
{
    public class UserViewModel
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public DateTime Birthday { get; set; }
        public bool Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ImgAvatar { get; set; }
        public bool IsActive { get; set; } = true;
        public long RoleId { get; set; } = 1;
    }
}
