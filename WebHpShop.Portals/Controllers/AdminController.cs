using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebHpShop.Domain.Entities;
using WebHpShop.Portals.Hepler;

namespace WebHpShop.Portals.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        
        {
            if (SessionHelper.GetSession<User>(HttpContext.Session, "Login") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var login = SessionHelper.GetSession<User>(HttpContext.Session, "Login");
                ViewBag.UserName = login.Username;
                return View();
            }
        }
    }
}
