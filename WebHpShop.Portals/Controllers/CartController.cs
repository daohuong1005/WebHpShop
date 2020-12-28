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
    public class CartController : Controller
    {
        private readonly WebHpShopDbContext dbContext;
        public CartController(WebHpShopDbContext sdbContext)
        {
            dbContext = sdbContext;
           
        }

        public IActionResult Index()
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
                ViewBag.SumProduct = cart.Count();
                return View();
            }
        }
        private int IsExits(int id)
        {
            List<CartDto> orderDetail = SessionHelper.GetSession<List<CartDto>>(HttpContext.Session, "cart");
            for (int i = 0; orderDetail.Count > i; i++)
            {
                if (orderDetail[i].Id.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
        public IActionResult BuyProducts(int id, int quantity)
        {

            var discount = (from p in dbContext.Discounts
                           where p.ProductId.Equals(id) && p.DiscountDateStart < DateTime.Now && p.DiscountDateEnd > DateTime.Now
                           select new
                           {
                               p.ProductId,
                               p.DiscountMoney,
                               
                           }).FirstOrDefault();

            var product = (from p in dbContext.Products
                           join i in dbContext.Images on p.ProductId equals i.ProductId
                           where p.ProductId.Equals(id)
                           select new
                           {
                               p.ProductId,p.ProductName,p.CostSell,i.Url
                           }).FirstOrDefault();

            if (SessionHelper.GetSession<List<CartDto>>(HttpContext.Session, "cart") == null)
            {
                List<CartDto> cart = new List<CartDto>();
                if (discount?.ProductId != null && discount.ProductId == id)
                {
                    cart.Add(new CartDto
                    {

                        Id = product.ProductId,
                        ProducName = product.ProductName,
                        Date = DateTime.Now,
                        PathImage = product.Url,
                        Price = product.CostSell - (product.CostSell / 100 * discount.DiscountMoney),
                        Quantity = quantity
                    });
                }
                else
                {
                    cart.Add(new CartDto
                    {

                        Id = product.ProductId,
                        ProducName = product.ProductName,
                        Date = DateTime.Now,
                        PathImage = product.Url,
                        Price = product.CostSell,
                        Quantity = quantity
                    });
                }    
                SessionHelper.SetSession(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<CartDto> cart = SessionHelper.GetSession<List<CartDto>>(HttpContext.Session, "cart");
                int index = IsExits(id);
                if (index != -1)
                {
                    cart[index].Quantity = cart[index].Quantity + quantity;
                }
                else
                {
                    if (discount.ProductId == id)
                    {
                        cart.Add(new CartDto
                        {

                            Id = product.ProductId,
                            ProducName = product.ProductName,
                            Date = DateTime.Now,
                            PathImage = product.Url,
                            Price = product.CostSell - (product.CostSell / 100 * discount.DiscountMoney),
                            Quantity = quantity
                        });
                    }
                    else
                    {
                        cart.Add(new CartDto
                        {

                            Id = product.ProductId,
                            ProducName = product.ProductName,
                            Date = DateTime.Now,
                            PathImage = product.Url,
                            Price = product.CostSell,
                            Quantity = quantity
                        });
                    }
                }
                SessionHelper.SetSession(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }
        public IActionResult BuyProducts2(int id)
        {
            var discount = (from p in dbContext.Discounts
                            where p.ProductId.Equals(id) && p.DiscountDateStart < DateTime.Now && p.DiscountDateEnd > DateTime.Now
                            select new
                            {
                                p.ProductId,
                                p.DiscountMoney,

                            }).FirstOrDefault();
            var product = (from p in dbContext.Products
                           join i in dbContext.Images on p.ProductId equals i.ProductId
                           where p.ProductId.Equals(id)
                           select new
                           {
                               p.ProductId,
                               p.ProductName,
                               p.CostSell,
                               i.Url
                           }).FirstOrDefault();
            if (SessionHelper.GetSession<List<CartDto>>(HttpContext.Session, "cart") == null)
            {
                List<CartDto> cart = new List<CartDto>();

                if (discount?.ProductId != null && discount.ProductId == id)
                {
                    cart.Add(new CartDto
                    {

                        Id = product.ProductId,
                        ProducName = product.ProductName,
                        Date = DateTime.Now,
                        PathImage = product.Url,
                        Price = product.CostSell - (product.CostSell / 100 * discount.DiscountMoney),
                        Quantity = 1
                    });
                }
                else
                {
                    cart.Add(new CartDto
                    {

                        Id = product.ProductId,
                        ProducName = product.ProductName,
                        Date = DateTime.Now,
                        PathImage = product.Url,
                        Price = product.CostSell,
                        Quantity = 1
                    });
                }
                SessionHelper.SetSession(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<CartDto> cart = SessionHelper.GetSession<List<CartDto>>(HttpContext.Session, "cart");
                int index = IsExits(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    if (discount.ProductId != null && discount.ProductId == id)
                    {
                        cart.Add(new CartDto
                        {

                            Id = product.ProductId,
                            ProducName = product.ProductName,
                            Date = DateTime.Now,
                            PathImage = product.Url,
                            Price = product.CostSell - (product.CostSell / 100 * discount.DiscountMoney),
                            Quantity = 1
                        });
                    }
                    else
                    {
                        cart.Add(new CartDto
                        {

                            Id = product.ProductId,
                            ProducName = product.ProductName,
                            Date = DateTime.Now,
                            PathImage = product.Url,
                            Price = product.CostSell,
                            Quantity = 1
                        });
                    }
                }
                SessionHelper.SetSession(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }
        public IActionResult BuyProductsUp(int id)
        {
            var product = (from p in dbContext.Products
                           join i in dbContext.Images on p.ProductId equals i.ProductId
                           where p.ProductId.Equals(id)
                           select new
                           {
                               p.ProductId,
                               p.ProductName,
                               p.CostSell,
                               i.Url
                           }).FirstOrDefault();
                List<CartDto> cart = SessionHelper.GetSession<List<CartDto>>(HttpContext.Session, "cart");
                int index = IsExits(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }

            SessionHelper.SetSession(HttpContext.Session, "cart", cart);
            
            return RedirectToAction("Index");
        }

        public IActionResult BuyProductsDown(int id)
        {
            var product = (from p in dbContext.Products
                           join i in dbContext.Images on p.ProductId equals i.ProductId
                           where p.ProductId.Equals(id)
                           select new
                           {
                               p.ProductId,
                               p.ProductName,
                               p.CostSell,
                               i.Url
                           }).FirstOrDefault();
            List<CartDto> cart = SessionHelper.GetSession<List<CartDto>>(HttpContext.Session, "cart");
            int index = IsExits(id);
            if (index != -1)
            {
                if(cart[index].Quantity == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                cart[index].Quantity--;
            }

            SessionHelper.SetSession(HttpContext.Session, "cart", cart);

            return RedirectToAction("Index");
        }

        public IActionResult RemoveProduct(int id)
        {
            List<CartDto> cart = SessionHelper.GetSession<List<CartDto>>(HttpContext.Session, "cart");
            int index = IsExits(id);
            cart.RemoveAt(index);
            SessionHelper.SetSession(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }
    }
}
