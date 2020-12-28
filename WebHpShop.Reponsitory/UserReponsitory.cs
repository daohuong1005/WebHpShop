using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WebHpShop.Common.Authentication;
using WebHpShop.Domain.Entities;
using WebHpShop.Reponsitory;
using WebHpShop.Reponsitory.Dto;
using WebHpShop.Reponsitory.Interfaces;
using WebStore.Domain;

namespace WebStore.Reponsitory
{
    public class UserReponsitory : BaseReponsitory<User>, IUserReponsitory
    {
        public UserReponsitory(WebHpShopDbContext dbcontext) : base(dbcontext)
        {

        }
        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = DbContext.Users.SingleOrDefault(x => x.Username == username);

            // check if username exists
             
            if (user == null)
                return null;
            if (user.RoleId == 3)
                return null;
            if (user.IsActive == false)
                return null;
            // check if password is correct
            if (!AuthenticationUser.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }
        public User Authenticate2(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = DbContext.Users.SingleOrDefault(x => x.Username == username);

            // check if username exists
            if (user == null)
                return null;
            if (user.IsActive == false)
                return null;

            // check if password is correct
            if (!AuthenticationUser.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        public int Create(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return 1;

            if (DbContext.Users.Any(x => x.Username == user.Username))
                return -1;
            byte[] passwordHash;
            byte[] passwordSalt;

            AuthenticationUser.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.ImgAvatar = "0000userPng.png";
            DbContext.Users.Add(user);
            DbContext.SaveChanges();

            return 2;
        }
        public int Create2(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return 1;

            if (DbContext.Users.Any(x => x.Username == user.Username))
                return -1;
            byte[] passwordHash;
            byte[] passwordSalt;

            AuthenticationUser.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.RoleId = 3;
            user.ImgAvatar = "0000userPng.png";
            DbContext.Users.Add(user);
            DbContext.SaveChanges();

            return 2;
        }

        private string UploadedFile(UserDto userDto)
        {
            string newFileName = null;

            if (userDto.ImageAvt != null)
            {
                var fileName = Path.GetFileName(userDto.ImageAvt.FileName);
                var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                var fileExtension = Path.GetExtension(fileName);
                newFileName = String.Concat(myUniqueFileName, fileExtension);
                var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images")).Root + $@"\{newFileName}";
                using (FileStream fs = System.IO.File.Create(filepath))
                {
                    userDto.ImageAvt.CopyTo(fs);
                    fs.Flush();
                }
            }
            return newFileName;
        }

        public void Update(UserDto userParam, string password = null)
        {
            var user = DbContext.Users.Find(userParam.UserId);
            if (user == null)
                throw new Exception("User not found");

            if (userParam.Username != user.Username)
            {
                // username has changed so check if the new username is already taken
                if (DbContext.Users.Any(x => x.Username == userParam.Username))
                    throw new Exception("Username " + userParam.Username + " is already taken");
                
            }
            
            userParam.ImgAvatar = UploadedFile(userParam);
            if(userParam.ImgAvatar == null)
            {
                userParam.ImgAvatar = user.ImgAvatar;
            }    
            // update user properties
            user.FirstName = userParam.FirstName;
            user.LastName = userParam.LastName;
            user.Username = userParam.Username;
            user.Address = userParam.Address;
            user.Birthday = userParam.Birthday;
            user.ImgAvatar = userParam.ImgAvatar;
            user.Email = userParam.Email;
            user.Phone = userParam.Phone;
            user.RoleId = userParam.RoleId;

            user.UpdateDate = DateTime.Now;
            

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                AuthenticationUser.CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            DbContext.Users.Update(user);
            DbContext.SaveChanges();
        }

        public void Delete(long id)
        {
            var user = DbContext.Users.Find(id);
            if (user != null)
            {
                user.IsActive = false;
                user.IsDelete = true;
                DbContext.Users.Update(user);
                DbContext.SaveChanges();
            }
        }

        public void Active(long id)
        {
            var user = DbContext.Users.Find(id);
            if (user != null)
            {
                user.IsActive = !user.IsActive;
                DbContext.Users.Update(user);
                DbContext.SaveChanges();
            }
        }
    }
}
