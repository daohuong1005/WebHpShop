using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebHpShop.Domain.Entities;
using WebHpShop.Portals.Hepler;
using WebHpShop.Reponsitory.Dto;
using WebHpShop.Service.Interfaces;
using WebStore.Domain;

namespace WebHpShop.Portals.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly WebHpShopDbContext _dbcontext;
        private IUserService _userService;
        private readonly IMapper _mapper;
        public CheckOutController(IUserService userService, WebHpShopDbContext sdbContext, IMapper mapper)
        {
            _dbcontext = sdbContext;
            _userService = userService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            if (SessionHelper.GetSession<User>(HttpContext.Session, "Login2") == null)
            {
                return RedirectToAction("Login2","User");
            }
            else
            {
                var cart = SessionHelper.GetSession<List<CartDto>>(HttpContext.Session, "cart");
                if (cart == null)
                {
                    return View();
                }
                else
                {
                    ViewBag.cart = cart;
                    ViewBag.total = cart.Sum(x => x.Price * x.Quantity);
                    var user = SessionHelper.GetSession<User>(HttpContext.Session, "Login2");
                    var userDto = _mapper.Map<UserDto>(user);
                    return View(userDto);
                }
            }
        }
        public IActionResult BuyNow(UserDto userdto,string password)
        {

            var cart = SessionHelper.GetSession<List<CartDto>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(x => x.Price * x.Quantity);

            var userSS = SessionHelper.GetSession<User>(HttpContext.Session, "Login2");

            //userSS.UserId = user.UserId;
            //userSS.FirstName = user.FirstName;
            //userSS.LastName = user.LastName;
            //userSS.Address = user.Address;
            //userSS.Email = user.Email;
            //userSS.Phone = user.Phone;
            //userSS.UpdateDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                _userService.Update(userdto, password);
            }

            var users =_userService.GetById(userSS.UserId);
            SessionHelper.SetSession(HttpContext.Session, "Login2", users);

            Order order = new Order();
            if (userdto.OrderAddress == null)
            {
                order = new Order()
                {
                    UserId = userSS.UserId,
                    OrderDate = DateTime.Now,
                    Total = ViewBag.total,
                    StatusId = 1,
                    OrderAddress = users.Address
                };
            }
            else
            {
                order = new Order()
                {
                    UserId = userSS.UserId,
                    OrderDate = DateTime.Now,
                    Total = ViewBag.total,
                    StatusId = 1,
                    OrderAddress = userdto.OrderAddress
                };
            }
            _dbcontext.Orders.Add(order);
            _dbcontext.SaveChanges();

            foreach (var i in cart)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    OrderId = order.OrderId,
                    ProductId = i.Id,
                    Quantity = i.Quantity,
                    Price =i.Price
                };
                _dbcontext.OrderDetails.Add(orderDetail);
                _dbcontext.SaveChanges();
            }
            SessionHelper.SetSession(HttpContext.Session, "cart", cart = null);

            return RedirectToAction("Index","Home");
        }


       
    }
}
