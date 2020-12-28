using System;
using System.Collections.Generic;
using System.Text;
using WebHpShop.Domain.Entities;
using WebHpShop.Reponsitory.Dto;

namespace WebHpShop.Service.Interfaces
{
    public interface IUserService : IServices<User>
    {
        User Authenticate(string username, string password);
        User Authenticate2(string username, string password);
        //IEnumerable<User> GetAll();
        //User GetById(int id);
        int Create(UserDto user, string password);
        int Create2(UserDto user, string password);
        void Update(UserDto user, string password = null);
        void Delete(long id);
        void Active(long id);
    }
}
