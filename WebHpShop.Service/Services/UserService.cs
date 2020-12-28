using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WebHpShop.Domain.Entities;
using WebHpShop.Reponsitory.Interfaces;
using WebHpShop.Reponsitory.Dto;
using WebHpShop.Service.Interfaces;
using WebHpShop.Service.Services;
using System.IO;
using Microsoft.Extensions.FileProviders;

namespace WebHpShop.Service
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IUserReponsitory _userReponsitory;
        private readonly IMapper _mapper;
        public UserService(IUserReponsitory userReponsitory, IMapper mapper) : base(userReponsitory)
        {
            _userReponsitory = userReponsitory;
            _mapper = mapper;
        }

        public User Authenticate(string username, string password)
        {
            return _userReponsitory.Authenticate(username, password);
        }
        public User Authenticate2(string username, string password)
        {
            return _userReponsitory.Authenticate2(username, password);
        }

        public int Create(UserDto userDto, string password)
        {
            var user = _mapper.Map<User>(userDto);
            return _userReponsitory.Create(user, password);
        }

        public int Create2(UserDto userDto, string password)
        {
            var user = _mapper.Map<User>(userDto);
            return _userReponsitory.Create2(user, password);
        }

        public void Delete(long id)
        {
            _userReponsitory.Delete(id);
        }

        public void Update(UserDto userDto, string password = null)
        {
            var user = _mapper.Map<User>(userDto);
            _userReponsitory.Update(userDto, password);
        }

        public void Active(long id)
        {
            _userReponsitory.Active(id);
        }


    }
}
