using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebHpShop.Reponsitory.Dto;
using WebHpShop.Service.Interface;
using WebStore.Domain;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebHpShop.Portals.Hepler;
using WebHpShop.Domain.Entities;

namespace WebHpShop.Portals.Controllers
{
    public class DiscountController : Controller
    {
        private IDiscountService _discountService;
        private readonly IMapper _mapper;
        private readonly WebHpShopDbContext _dbcontext;

        public DiscountController(IDiscountService discountService, IMapper mapper, WebHpShopDbContext dbcontext)
        {
            _discountService = discountService;
            _mapper = mapper;
            _dbcontext = dbcontext;
        }
        public async Task<IActionResult> Index()
        {
            if (SessionHelper.GetSession<User>(HttpContext.Session, "Login") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var discount = await _discountService.GetAllAsync();
                var discountDto = _mapper.Map<IList<DiscountDto>>(discount);
                return View(discountDto);
            }
        }

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

                // Getting all supplier data 


                var discountData = (from d in _dbcontext.Discounts
                                    join p in _dbcontext.Products on d.ProductId equals p.ProductId
                                    where d.IsDelete == false
                                    where d.IsDisplay == true
                                    select new {d.DiscountId,d.DiscountContent,d.DiscountDateStart,d.DiscountDateEnd,d.DiscountMoney,p.ProductName  });

                //Sort data
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    discountData = discountData.OrderBy(sortColumn + " " + sortColumnDirection);
                    //(sortColumn + " " + sortColumnDirection);
                }
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    discountData = discountData.Where(m => m.DiscountContent.Contains(searchValue));
                }

                //total number of rows count   
                recordsTotal = discountData.Count();

                //Paging   
                var data = discountData.Skip(skip).Take(pageSize).ToList();

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
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_dbcontext.Products.Where(p => p.IsDelete == false)
                                                                       .Where(p => p.IsDisplay == true), "ProductId", "ProductName");
            return View();
        }
        [HttpPost]
        public IActionResult Create(DiscountDto discountDto)
        {
            if (SessionHelper.GetSession<User>(HttpContext.Session, "Login") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var reulst = _discountService.Create(discountDto);
                    return RedirectToAction("Index");
                }
                ViewData["ProductId"] = new SelectList(_dbcontext.Products.Where(p => p.IsDelete == false)
                                                                           .Where(p => p.IsDisplay == true), "ProductId", "ProductName");
                return View();
            }
        }

        public async Task<IActionResult> Update(long id)
        {
            if (SessionHelper.GetSession<User>(HttpContext.Session, "Login") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {

                var discount = await _discountService.GetByIdAsync(id);
                var discountDto = _mapper.Map<DiscountDto>(discount);
                ViewData["ProductId"] = new SelectList(_dbcontext.Products.Where(p => p.IsDelete == false)
                                                                          .Where(p => p.IsDisplay == true), "ProductId", "ProductName", discountDto.ProductId);
                return View(discountDto);
            }
        }

        [HttpPost]
        public IActionResult Update(DiscountDto discountDto)
        {
                if (ModelState.IsValid)
                {
                    _discountService.UpdateDiscount(discountDto);
                    return RedirectToAction("Index");
                }
                ViewData["ProductId"] = new SelectList(_dbcontext.Products.Where(p => p.IsDelete == false)
                                                                          .Where(p => p.IsDisplay == true), "ProductId", "ProductName", discountDto.ProductId);
                return View();
        }

        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (SessionHelper.GetSession<User>(HttpContext.Session, "Login") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var discount = await _discountService.GetByIdAsync(id);
                if (discount != null)
                {
                    _discountService.DeleteDiscount(id);
                    return RedirectToAction("Index");
                }
                return StatusCode(404);
            }
        }

        
    }
}
