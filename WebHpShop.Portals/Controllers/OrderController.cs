using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebHpShop.Domain.Entities;
using WebHpShop.Portals.Hepler;
using WebStore.Domain;
using System.Linq.Dynamic.Core;
using WebHpShop.Service.ViewModel;

namespace WebHpShop.Portals.Controllers
{
    public class OrderController : Controller
    {
        private readonly WebHpShopDbContext _dbcontext;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IMapper _mapper;

        public OrderController(WebHpShopDbContext context, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            _dbcontext = context;
            webHostEnvironment = hostEnvironment;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            if (SessionHelper.GetSession<User>(HttpContext.Session, "Login") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                return View(await _dbcontext.Orders.ToListAsync());
            }
        }
        public async Task<IActionResult> Index1()
        {
            if (SessionHelper.GetSession<User>(HttpContext.Session, "Login") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                return View(await _dbcontext.Orders.ToListAsync());
            }
        }

        public async Task<IActionResult> Index2()
        {
            if (SessionHelper.GetSession<User>(HttpContext.Session, "Login") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                return View(await _dbcontext.Orders.ToListAsync());
            }
        }

        public async Task<IActionResult> Index3()
        {
            if (SessionHelper.GetSession<User>(HttpContext.Session, "Login") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                return View(await _dbcontext.Orders.ToListAsync());
            }
        }
        

        public async Task<IActionResult> UserOrder()
        {
            if (SessionHelper.GetSession<User>(HttpContext.Session, "Login2") == null)
            {
                return RedirectToAction("Login2", "User");
            }
            else
            {
                return View(await _dbcontext.Orders.ToListAsync());
            }
        }

        public async Task<IActionResult> UserOrderDelete()
        {
            if (SessionHelper.GetSession<User>(HttpContext.Session, "Login2") == null)
            {
                return RedirectToAction("Login2", "User");
            }
            else
            {
                return View(await _dbcontext.Orders.ToListAsync());
            }
        }

        public IActionResult LoadData2()
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

                // Getting all supplier data 


                var orderData = (from o in _dbcontext.Orders
                                 join u in _dbcontext.Users on o.UserId equals u.UserId
                                 join s in _dbcontext.Statuses on o.StatusId equals s.Id
                                 where o.IsDelete == false
                                 select new { o.OrderId, u.Username, o.OrderDate, s.StatusName });
                //Sort data
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    orderData = orderData.OrderBy(sortColumn + " " + sortColumnDirection);
                    //(sortColumn + " " + sortColumnDirection);
                }
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    orderData = orderData.Where(m => m.Username.Contains(searchValue));
                }

                //total number of rows count   
                recordsTotal = orderData.Count();

                //Paging   
                var data = orderData.Skip(skip).Take(pageSize).ToList();

                //Returning Json Data  
                var json = Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
                return json;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IActionResult LoadData([FromForm] int Data)
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

                //// Sort Column Direction ( asc ,desc)  
                //var sortColumnDirection = Request.Form["order[0][asc]"].FirstOrDefault();

                // Search Value from (Search box)  
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                //Paging Size (10,20,50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                // Getting all supplier data 


                var orderData = (from o in _dbcontext.Orders
                                 join u in _dbcontext.Users on o.UserId equals u.UserId
                                 join s in _dbcontext.Statuses on o.StatusId equals s.Id
                                 orderby o.OrderId descending
                                 where o.StatusId == Data && o.IsDelete == false
                                 select new { o.OrderId, u.Username, o.OrderDate, s.StatusName });
                //Sort data
                //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                //{
                //    orderData = orderData.OrderBy(sortColumn + " " + sortColumnDirection);
                //    //(sortColumn + " " + sortColumnDirection);
                //}
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    orderData = orderData.Where(m => m.Username.Contains(searchValue));
                }

                //total number of rows count   
                recordsTotal = orderData.Count();

                //Paging   
                var data = orderData.Skip(skip).Take(pageSize).ToList();

                //Returning Json Data  
                var json = Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
                return json;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IActionResult LoadDataUser([FromForm] int Data)
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

                //// Sort Column Direction ( asc ,desc)  
                //var sortColumnDirection = Request.Form["order[0][asc]"].FirstOrDefault();

                // Search Value from (Search box)  
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                //Paging Size (10,20,50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                // Getting all supplier data 
                var x = SessionHelper.GetSession<User>(HttpContext.Session, "Login2");

                var orderData = (from o in _dbcontext.Orders
                                 join u in _dbcontext.Users on o.UserId equals u.UserId
                                 join s in _dbcontext.Statuses on o.StatusId equals s.Id
                                 orderby o.OrderId descending
                                 where o.StatusId == Data && o.IsDelete == false && o.UserId == x.UserId
                                 select new { o.OrderId, u.Username, o.OrderDate, s.StatusName });
                //Sort data
                //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                //{
                //    orderData = orderData.OrderBy(sortColumn + " " + sortColumnDirection);
                //    //(sortColumn + " " + sortColumnDirection);
                //}
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    orderData = orderData.Where(m => m.Username.Contains(searchValue));
                }

                //total number of rows count   
                recordsTotal = orderData.Count();

                //Paging   
                var data = orderData.Skip(skip).Take(pageSize).ToList();

                //Returning Json Data  
                var json = Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
                return json;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IActionResult LoadDataUser2()
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
                //var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();

                // Search Value from (Search box)  
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                //Paging Size (10,20,50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                // Getting all supplier data 
                var x = SessionHelper.GetSession<User>(HttpContext.Session, "Login2");


                var orderData = (from o in _dbcontext.Orders
                                 join u in _dbcontext.Users on o.UserId equals u.UserId
                                 join s in _dbcontext.Statuses on o.StatusId equals s.Id
                                 orderby o.OrderId descending
                                 where o.IsDelete == false && o.UserId == x.UserId
                                 select new { o.OrderId, u.Username, o.OrderDate, s.StatusName });
                ////Sort data
                //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                //{
                //    orderData = orderData.OrderBy(sortColumn + " " + sortColumnDirection);
                //    //(sortColumn + " " + sortColumnDirection);
                //}
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    orderData = orderData.Where(m => m.Username.Contains(searchValue));
                }

                //total number of rows count   
                recordsTotal = orderData.Count();

                //Paging   
                var data = orderData.Skip(skip).Take(pageSize).ToList();

                //Returning Json Data  
                var json = Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
                return json;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> ConfirmedOrder([FromForm] long Id)
        {

            try
            {
                if (SessionHelper.GetSession<User>(HttpContext.Session, "Login") == null)
                {
                    return RedirectToAction("Login", "User");
                }
                else
                {
                    var result = await _dbcontext.Orders.FindAsync(Id);
                    if (result != null)
                    {
                        result.StatusId = 2;
                        result.UpdateDate = DateTime.Now;
                        _dbcontext.Orders.Update(result);
                        _dbcontext.SaveChanges();
                        return View(nameof(Index));
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }

        }

        public async Task<IActionResult> OrderDone([FromForm] long Id)
        {
            try
            {
                if (SessionHelper.GetSession<User>(HttpContext.Session, "Login") == null)
                {
                    return RedirectToAction("Login", "User");
                }
                else
                {
                    var result = await _dbcontext.Orders.FindAsync(Id);
                    if (result != null)
                    {
                        result.StatusId = 4;
                        result.UpdateDate = DateTime.Now;
                        _dbcontext.Orders.Update(result);
                        _dbcontext.SaveChanges();
                        return View(nameof(Index3));
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }

        }

        public async Task<IActionResult> DeletedOrder([FromForm] long Id)
        {
            try
            {
                if (SessionHelper.GetSession<User>(HttpContext.Session, "Login") == null)
                {
                    return RedirectToAction("Login", "User");
                }
                else
                {
                    var result = await _dbcontext.Orders.FindAsync(Id);
                    if (result != null)
                    {
                        result.StatusId = 3;
                        result.UpdateDate = DateTime.Now;
                        _dbcontext.Orders.Update(result);
                        _dbcontext.SaveChanges();
                        return View(nameof(Index));
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }

        }


        public async Task<IActionResult> UserDeletedOrder([FromForm] long Id)
        {
            try
            {
                if (SessionHelper.GetSession<User>(HttpContext.Session, "Login2") == null)
                {
                    return RedirectToAction("Login2", "User");
                }
                else
                {
                    var result = await _dbcontext.Orders.FindAsync(Id);
                    if (result != null)
                    {
                        result.StatusId = 3;
                        result.UpdateDate = DateTime.Now;
                        _dbcontext.Orders.Update(result);
                        _dbcontext.SaveChanges();
                        return RedirectToAction("CustomerDetail","User",new {@id = result.UserId});
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }

        }
        public IActionResult DetailOrder (long id)
        {
            var Order = from p in _dbcontext.Orders
                              join u in _dbcontext.Users on p.UserId equals u.UserId
                              join i in _dbcontext.Statuses on p.StatusId equals i.Id
                              where p.OrderId == id
                              select new OrderViewModel()
                              {
                                  OrderId = p.OrderId,
                                  OrderDate = p.OrderDate,
                                  ShipDate = p.ShipDate,
                                  StatusId = p.StatusId,
                                  UserId = p.UserId,
                                  Total = p.Total,
                                  UserName = u.Username,
                                  FirsName= u.FirstName,
                                  LastName= u.LastName,
                                  Phone = u.Phone,
                                  Address = u.Address,
                                  Gender = u.Gender,
                                  Bithday = u.Birthday,
                                  NameStt = i.StatusName,
                                  OrderAdress = p.OrderAddress
                              };
            ViewBag.Orderss = Order;
            var OrderDetail = from p in _dbcontext.OrderDetails
                              join pr in _dbcontext.Products on p.ProductId equals pr.ProductId
                              join a in _dbcontext.Images on pr.ProductId equals a.ProductId
                              where p.OrderId == id
                              where a.IsDisplay == true && a.IsDelete == false
                              select new OrderDetailViewModel()
                              {
                                  OrderId = p.OrderId,
                                  ProductId=  p.ProductId,
                                  ProductName = pr.ProductName,
                                  Price = p.Price,
                                  Quantity = p.Quantity,
                                  Img = a.Url
                              };
            ViewBag.OrderDetail = OrderDetail;
            return View(Order);
        }


        public IActionResult DetailOrder2(long id)
        {
            var Order = from p in _dbcontext.Orders
                        join u in _dbcontext.Users on p.UserId equals u.UserId
                        join i in _dbcontext.Statuses on p.StatusId equals i.Id
                        where p.OrderId == id
                        select new OrderViewModel()
                        {
                            OrderId = p.OrderId,
                            OrderDate = p.OrderDate,
                            ShipDate = p.ShipDate,
                            StatusId = p.StatusId,
                            UserId = p.UserId,
                            Total = p.Total,
                            UserName = u.Username,
                            FirsName = u.FirstName,
                            LastName = u.LastName,
                            Phone = u.Phone,
                            Address = u.Address,
                            Gender = u.Gender,
                            Bithday = u.Birthday,
                            NameStt = i.StatusName,
                            OrderAdress = p.OrderAddress
                            
                        };
            ViewBag.Orderss = Order;
            var OrderDetail = from p in _dbcontext.OrderDetails
                              join pr in _dbcontext.Products on p.ProductId equals pr.ProductId
                              join a in _dbcontext.Images on pr.ProductId equals a.ProductId
                              where p.OrderId == id
                              where a.IsDisplay == true && a.IsDelete == false
                              select new OrderDetailViewModel()
                              {
                                  OrderId = p.OrderId,
                                  ProductId = p.ProductId,
                                  ProductName = pr.ProductName,
                                  Price = p.Price,
                                  Quantity = p.Quantity,
                                  Img = a.Url
                              };
            ViewBag.OrderDetail = OrderDetail;
            return View(Order);
        }
        public IActionResult BaoCaoXuat()
        {
            var Order = from p in _dbcontext.Orders
                        join u in _dbcontext.Users on p.UserId equals u.UserId
                        join i in _dbcontext.Statuses on p.StatusId equals i.Id
                        select new OrderViewModel()
                        {
                            OrderId = p.OrderId,
                            OrderDate = p.OrderDate,
                            ShipDate = p.ShipDate,
                            StatusId = p.StatusId,
                            UserId = p.UserId,
                            Total = p.Total,
                            UserName = u.Username,
                            FirsName = u.FirstName,
                            LastName = u.LastName,
                            Phone = u.Phone,
                            Address = u.Address,
                            Gender = u.Gender,
                            Bithday = u.Birthday,
                            NameStt = i.StatusName
                        };

            ViewBag.Orderss = Order;
            var OrderDetail = from p in _dbcontext.OrderDetails
                              join pr in _dbcontext.Products on p.ProductId equals pr.ProductId
                              select new OrderDetailViewModel()
                              {
                                  OrderId = p.OrderId,
                                  ProductId = p.ProductId,
                                  ProductName = pr.ProductName,
                                  Price = p.Price,
                                  Quantity = p.Quantity,
                              };
            ViewBag.OrderDetail = OrderDetail;
            return View(Order);
        }
    }
}
