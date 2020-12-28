using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebHpShop.Common.Authentication;
using WebHpShop.Domain.Entities;
using WebHpShop.Reponsitory.Dto;
using WebHpShop.Service.Interfaces;
using WebHpShop.Service.ViewModel;
using WebStore.Domain;
using System.Linq.Dynamic.Core;
using WebHpShop.Portals.Hepler;

namespace WebHpShop.Portals.Controllers
{
    public class UserController : Controller
    {
        private IUserService _userService;
        private readonly AuthencationSetting _authenticationSetting;
        private readonly IMapper _mapper;
        private readonly WebHpShopDbContext _dbcontext;

        public UserController(IUserService userService, IOptions<AuthencationSetting> authencationSetting, IMapper mapper, WebHpShopDbContext dbcontext)
        {
            _userService = userService;
            _authenticationSetting = authencationSetting.Value;
            _mapper = mapper;
            _dbcontext = dbcontext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (SessionHelper.GetSession<User>(HttpContext.Session, "Login") == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var user = await _userService.GetAllAsync();
                var userViewModel = _mapper.Map<IList<UserViewModel>>(user);
                return View(userViewModel);
            }
        }
        [HttpPost]
        public IActionResult LoadData()
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();

                // Skiping number of Rows count  
                var start = Request.Form["start"].FirstOrDefault();

                // Paging Length 10,20  
                var length = Request.Form["length"].FirstOrDefault();

                // Sort Column Name  
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();

                // Sort Column Direction ( asc ,desc)  
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();

                // Search Value from (Search box)  
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                //Paging Size (10,20,50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                // Getting all product data 

                var userData = (from p in _dbcontext.Users
                                join r in _dbcontext.Roles on p.RoleId equals r.RoleId
                                where p.IsDelete == false
                                select new { p.UserId, p.FirstName,p.IsActive, p.LastName, p.Username, p.Email, p.Phone, p.Birthday, p.Address, p.Gender,r.Name }); ;
                //Sort data
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    userData = userData.OrderBy(sortColumn + " " + sortColumnDirection);
                    //(sortColumn + " " + sortColumnDirection);
                }
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    userData = userData.Where(m => m.Username.Contains(searchValue));

                   
                }

                //total number of rows count   
                recordsTotal = userData.Count();

                //Paging   
                var data = userData.Skip(skip).Take(pageSize).ToList();

                //Returning Json Data  
                var json = Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
                return json;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserDto userDto)
        {
           
                var user = _userService.Create2(userDto, userDto.Password);
            if (ModelState.IsValid)
            {
                if (user == 1)
                {
                    ModelState.AddModelError("", "Mật Khẩu Không Được Để Khoảng Trắng");
                }
                else if (user == -1)
                {
                    ModelState.AddModelError("", "Tên Tài Khoản Đã Có Người Dùng");
                }
                else
                {
                    return RedirectToAction("Login2","User");
                }
            }
                return View();
            
        }


        [HttpGet]
        public IActionResult Create()
        {
            if (SessionHelper.GetSession<User>(HttpContext.Session, "Login") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserDto userDto)
        {
            
            var user = _userService.Create(userDto, userDto.Password);
            if (ModelState.IsValid)
            {
                if (user == 1)
                {
                    ModelState.AddModelError("", "Mật Khẩu Không Được Để Khoảng Trắng");
                }
                else if (user == -1)
                {
                    ModelState.AddModelError("", "Tên Tài Khoản Đã Có Người Dùng");
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
                return View();
            
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginDto loginDto)
        {
            var result = _userService.Authenticate(loginDto.Username, loginDto.Password);
            if (ModelState.IsValid)
            {
                if (result == null)
                {
                    ModelState.AddModelError("", "Mật Khẩu Hoặc Tài Khoản Không Đúng");
                }
                else
                {
                    SessionHelper.SetSession(HttpContext.Session, "Login", result);
                  
                    return RedirectToAction("Index","Admin");
                }
            }
            return View();

        }

        [HttpGet]
        public IActionResult Login2()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login2(LoginDto loginDto)
        {
            var result = _userService.Authenticate2(loginDto.Username, loginDto.Password);
            if (ModelState.IsValid)
            {
                if (result == null)
                {
                    ModelState.AddModelError("", "Mật Khẩu Hoặc Tài Khoản Không Đúng");
                }
                else
                {
                    SessionHelper.SetSession(HttpContext.Session, "Login2", result);
                    return RedirectToAction("Index","Home");
                }
            }
            return View();

        }

        [HttpGet]
        public IActionResult CustomerDetail()
        {
            var userSS = SessionHelper.GetSession<User>(HttpContext.Session, "Login2");
            var userViewModel = _mapper.Map<UserViewModel>(userSS);
            return View(userViewModel);
        }


        [HttpGet]
        public IActionResult UpdateCustomerDetail()
        {
            var userSS = SessionHelper.GetSession<User>(HttpContext.Session, "Login2");
            var userViewModel = _mapper.Map<UserDto>(userSS);
            return View(userViewModel);
        }

        [HttpPost]
        public IActionResult UpdateCustomerDetail(UserDto userDto, string password = null)
        {

            if(ModelState.IsValid)
            {
                _userService.Update(userDto, password);
                var result = _userService.GetById(userDto.UserId);
                SessionHelper.SetSession(HttpContext.Session, "Login2", result);
                return RedirectToAction("CustomerDetail");
            }
            return View(userDto);
        }



        public IActionResult Logout( )
        {
            var userSS = SessionHelper.GetSession<User>(HttpContext.Session, "Login2");
            SessionHelper.SetSession(HttpContext.Session, "Login2", userSS = null);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout2()
        {
            var userSS = SessionHelper.GetSession<User>(HttpContext.Session, "Login");
            SessionHelper.SetSession(HttpContext.Session, "Login", userSS = null);
            return RedirectToAction("Login", "User");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long Id)
        {
            if (SessionHelper.GetSession<User>(HttpContext.Session, "Login") == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                try
                {
                    var user = await _userService.GetByIdAsync(Id);
                    if (user != null)
                    {
                        _userService.Delete(Id);
                        return RedirectToAction("Index");
                    }
                    return StatusCode(404);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }    
            
        }
       


        public async Task<IActionResult> Update(long id)
        {
            var user = await _userService.GetByIdAsync(id);
            var userDto = _mapper.Map<UserDto>(user);
            return View(userDto);
        }
        [HttpPost]
        public IActionResult Update(UserDto userDto,string password = null)
        {
            if (SessionHelper.GetSession<User>(HttpContext.Session, "Login") == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _userService.Update(userDto, password);
                }
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Active(long id)
        {
            if (SessionHelper.GetSession<User>(HttpContext.Session, "Login") == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                try
                {
                    var user =  _userService.GetById(id);
                    if (user != null)
                    {
                        _userService.Active(id);
                        return RedirectToAction("Index");
                    }
                    return StatusCode(404);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}
