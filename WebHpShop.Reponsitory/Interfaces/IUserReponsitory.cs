using System;
using System.Collections.Generic;
using System.Text;
using WebHpShop.Domain.Entities;
using WebHpShop.Reponsitory.Dto;
using WebHpShop.Reponsitory.Interfaces;

namespace WebHpShop.Reponsitory.Interfaces
{
    public interface IUserReponsitory : IReponsitory<User>
    {
        User Authenticate(string Username, string password);
        User Authenticate2(string Username, string password);
        //IEnumerable<User> GetAll();
        //User GetById(int id);
        int Create(User user, string password);
        int Create2(User user, string password);
        void Update(UserDto userDto, string password = null);
        void Active(long id);
        void Delete(long id);
    }
}
