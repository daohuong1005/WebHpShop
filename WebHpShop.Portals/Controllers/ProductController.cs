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
using WebHpShop.Service.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace WebHpShop.Portals.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private readonly IMapper _mapper;
        private readonly WebHpShopDbContext _dbcontext;

        public ProductController(IProductService productService, IMapper mapper, WebHpShopDbContext dbcontext)
        {
            _productService = productService;
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
                var product = await _productService.GetAllAsync();
                var productDtos = _mapper.Map<IList<ProductDto>>(product);
                return View(productDtos);

            }
        }
        // Mới
        public async Task<IActionResult> TopSale()
        {
            try
            {
                DateTime dateTimeNow = DateTime.Now;
                var product = await ((from p in _dbcontext.Products
                                      join i in _dbcontext.Images on p.ProductId equals i.ProductId
                                      join d in _dbcontext.Discounts on p.ProductId equals d.ProductId
                                      where d.DiscountDateStart < dateTimeNow && d.DiscountDateEnd > dateTimeNow
                                      && i.IsDelete == false && i.IsDisplay == true 
                                      && p.IsDelete ==false && p.IsDisplay == true
                                      && d.DiscountMoney >= 10
                                      orderby p.ProductId descending,p.ProductName ascending
                                      select new ProductDiscounViewModel()
                                      {
                                          Id = p.ProductId,
                                          ProductName = p.ProductName,
                                          sale = d.DiscountMoney,
                                          DisscountMoney = p.CostSell - (p.CostSell / 100 * d.DiscountMoney),
                                          Image = i.Url,
                                          Price = p.CostSell
                                      }).Distinct().Take(15)).ToListAsync();
                return View(product);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
        // Mới
        public async Task<IActionResult> ProductNew()
        {

            var product = await ((from p in _dbcontext.Products
                                  join i in _dbcontext.Images on p.ProductId equals i.ProductId
                                  orderby p.ProductId descending
                                  where p.IsDisplay == true && p.IsDelete == false
                                  && i.IsDelete == false && i.IsDisplay == true
                                  select new ProductDiscounViewModel()
                                  {
                                      Id = p.ProductId,
                                      ProductName = p.ProductName,
                                      Image = i.Url,
                                      Price = p.CostSell

                                  }).Take(15)).ToListAsync();

            return View(product);
        }
        // Mới

        public async Task<IActionResult> AllPhone()
        {

            var product = await ((from p in _dbcontext.Products
                                  join i in _dbcontext.Images on p.ProductId equals i.ProductId
                                  join d in _dbcontext.Discounts on p.ProductId equals d.ProductId
                                  join c in _dbcontext.Categories on p.CategoryId equals c.CategoryId
                                  join m in _dbcontext.Manufacturers on p.ManufacturerId equals m.ManufacturerId
                                  where p.IsDisplay == true && p.IsDelete == false
                                  && d.IsDisplay == true && d.IsDelete == false
                                  && i.IsDelete == false && i.IsDisplay == true
                                  && m.IsDisplay == true && m.IsDelete == false
                                  && p.CategoryId == 1
                                  select new ProductDiscounViewModel()
                                  {
                                      Id = p.ProductId,
                                      ProductName = p.ProductName,
                                      sale = d.DiscountMoney,
                                      DisscountMoney = p.CostSell - (p.CostSell / 100 * d.DiscountMoney),
                                      Image = i.Url,
                                      Price = p.CostSell,
                                      ManufacturerName = m.ManufacturerName

                                  }).Distinct().Take(15)).ToListAsync();

            var maufu = await ((from m in _dbcontext.Manufacturers
                                join p in _dbcontext.Products on m.ManufacturerId equals p.ManufacturerId
                                join c in _dbcontext.Categories on p.CategoryId equals c.CategoryId
                               where p.IsDelete == false && p.IsDisplay == true
                               && c.CategoryId == 1
                                select new ManufacturerViewModel()
                                {
                                   manufacturerId = m.ManufacturerId,
                                   manufacturerName = m.ManufacturerName

                                }).Distinct().Take(15)).ToListAsync();
            ViewBag.Maufu = maufu;
           
            return View(product);
        }
        // Mới
        public async Task<IActionResult> AllLapTop()
        {

            var product = await ((from p in _dbcontext.Products
                                  join i in _dbcontext.Images on p.ProductId equals i.ProductId
                                  join d in _dbcontext.Discounts on p.ProductId equals d.ProductId
                                  join c in _dbcontext.Categories on p.CategoryId equals c.CategoryId
                                  join m in _dbcontext.Manufacturers on p.ManufacturerId equals m.ManufacturerId
                                  where p.IsDisplay == true && p.IsDelete == false
                                  && d.IsDisplay == true && d.IsDelete == false
                                  && i.IsDelete == false && i.IsDisplay == true
                                  && m.IsDisplay == true && m.IsDelete == false
                                  && p.CategoryId == 2
                                  select new ProductDiscounViewModel()
                                  {
                                      Id = p.ProductId,
                                      ProductName = p.ProductName,
                                      sale = d.DiscountMoney,
                                      DisscountMoney = p.CostSell - (p.CostSell / 100 * d.DiscountMoney),
                                      Image = i.Url,
                                      Price = p.CostSell,
                                      ManufacturerName = m.ManufacturerName

                                  }).Distinct().Take(15)).ToListAsync();

            var maufu = await ((from m in _dbcontext.Manufacturers
                                join p in _dbcontext.Products on m.ManufacturerId equals p.ManufacturerId
                                join c in _dbcontext.Categories on p.CategoryId equals c.CategoryId
                                where p.IsDelete == false && p.IsDisplay == true
                                && c.CategoryId == 2
                                select new ManufacturerViewModel()
                                {
                                    manufacturerId = m.ManufacturerId,
                                    manufacturerName = m.ManufacturerName

                                }).Distinct().Take(15)).ToListAsync();
            ViewBag.Maufu = maufu;

            return View(product);
        }
        // Mới
        public async Task<IActionResult> ProductDetail(long Id)
        {
            try
            {
                var product = await _dbcontext.Products.FindAsync(Id);
                if (product.IsDelete == false)
                {
                    _mapper.Map<ProductViewModel>(product);
                    ViewBag.Discount = await ((from d in _dbcontext.Discounts
                                               where d.ProductId.Equals(Id)
                                               select new DiscountViewModel()
                                               {
                                                   DiscountMoney = d.DiscountMoney
                                               })).ToListAsync();



                    ViewBag.ListImage = await ((from p in _dbcontext.Products
                                                join i in _dbcontext.Images on p.ProductId equals i.ProductId
                                                where p.ProductId.Equals(Id)
                                                select new ImageViewModel()
                                                {
                                                    Url = i.Url
                                                })).ToListAsync();

                    ViewBag.Image = await ((from p in _dbcontext.Products
                                            join i in _dbcontext.Images on p.ProductId equals i.ProductId
                                            where p.ProductId.Equals(Id) && i.IsDisplay == true && i.IsDelete == false
                                            select new ImageViewModel()
                                            {
                                                Url = i.Url
                                            })).ToListAsync();


                    ViewBag.Manufacture = await ((from p in _dbcontext.Products
                                                  join i in _dbcontext.Manufacturers on p.ManufacturerId equals i.ManufacturerId
                                                  where p.ProductId.Equals(Id)
                                                  select new ManufacturerViewModel()
                                                  {
                                                      manufacturerName = i.ManufacturerName
                                                  })).ToListAsync();


                    ViewBag.Category = await ((from p in _dbcontext.Products
                                               join i in _dbcontext.Categories on p.CategoryId equals i.CategoryId
                                               where p.ProductId.Equals(Id)
                                               select new CategoryViewModel()
                                               {
                                                   CatergoryName = i.CatergoryName
                                               })).ToListAsync();

                    ViewBag.Supplier = await ((from p in _dbcontext.Products
                                               join i in _dbcontext.Suppliers on p.SupplierId equals i.SupplierId
                                               where p.ProductId.Equals(Id)
                                               select new SupplierViewModel()
                                               {
                                                   SupplierName = i.SupplierName
                                               })).ToListAsync();

                    ViewBag.ListProductlq = await ((from b in _dbcontext.Products
                                                    where b.SupplierId == product.SupplierId
                                                    select new ProductViewModel()
                                                    {
                                                        ProductId = b.ProductId,
                                                        ProductName = b.ProductName,
                                                        CostSell = b.CostSell,

                                                    })).ToListAsync();
                    return View(_mapper.Map<ProductViewModel>(product));
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<IActionResult> Sreach(string Sreach)
        {
            if (Sreach == null)
            {
                return null;
            }
            else
            {
                var product = await ((from p in _dbcontext.Products
                                      join i in _dbcontext.Images on p.ProductId equals i.ProductId
                                      join c in _dbcontext.Categories on p.CategoryId equals c.CategoryId
                                      join m in _dbcontext.Manufacturers on p.ManufacturerId equals m.ManufacturerId
                                      where p.IsDisplay == true && p.IsDelete == false
                                      where i.IsDelete == false && i.IsDisplay == true
                                      where p.ProductName.Contains(Sreach)
                                      select new ProductDiscounViewModel()
                                      {
                                          Id = p.ProductId,
                                          ProductName = p.ProductName,
                                          Image = i.Url,
                                          Price = p.CostSell
                                      }).Distinct().Take(15)).ToListAsync();
                return View(product);
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


                var productData = (from p in _dbcontext.Products
                                    join c in _dbcontext.Categories on p.CategoryId equals c.CategoryId
                                    join s in _dbcontext.Suppliers on p.SupplierId equals s.SupplierId
                                    join m in _dbcontext.Manufacturers on p.ManufacturerId equals m.ManufacturerId
                                    where p.IsDelete == false
                                    where p.IsDisplay == true
                                    select new {p.ProductId,p.ProductName,c.CatergoryName,s.SupplierName,m.ManufacturerName, p.OperatingSystem,p.CPU,p.Ram,p.Rom,p.NameColor});

                //Sort data
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    productData = productData.OrderBy(sortColumn + " " + sortColumnDirection);
                    //(sortColumn + " " + sortColumnDirection);
                }
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    productData = productData.Where(m => m.ProductName.Contains(searchValue));
                }

                //total number of rows count   
                recordsTotal = productData.Count();

                //Paging   
                var data = productData.Skip(skip).Take(pageSize).ToList();

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
            if (SessionHelper.GetSession<User>(HttpContext.Session, "Login") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                ViewData["CategoryId"] = new SelectList(_dbcontext.Categories.Where(c => c.IsDelete == false).Where(s => s.IsDisplay == true), "CategoryId", "CatergoryName");
                ViewData["SupplierId"] = new SelectList(_dbcontext.Suppliers.Where(s => s.IsDelete == false).Where(s => s.IsDisplay == true), "SupplierId", "SupplierName");
                ViewData["ManufacturerId"] = new SelectList(_dbcontext.Manufacturers.Where(s => s.IsDelete == false).Where(s => s.IsDisplay == true), "ManufacturerId", "ManufacturerName");
                return View();
            }
        }
        [HttpPost]
        public IActionResult Create(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                var reulst = _productService.Create(productDto);
                return RedirectToAction("Index");
            }
            ViewData["CategoryId"] = new SelectList(_dbcontext.Categories.Where(c => c.IsDelete == false).Where(s => s.IsDisplay == true), "CategoryId", "CatergoryName", productDto.CategoryId);
            ViewData["SupplierId"] = new SelectList(_dbcontext.Suppliers.Where(s => s.IsDelete == false).Where(s => s.IsDisplay == true), "SupplierId", "SupplierName",productDto.SupplierId);
            ViewData["ManufacturerId"] = new SelectList(_dbcontext.Manufacturers.Where(s => s.IsDelete == false).Where(s => s.IsDisplay == true), "ManufacturerId", "ManufacturerName",productDto.ManufacturerId);
            return View();
        }

        public async Task<IActionResult> Update(long id)
        {
            if (SessionHelper.GetSession<User>(HttpContext.Session, "Login") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var product = await _productService.GetByIdAsync(id);
                var productDto = _mapper.Map<ProductDto>(product);
                ViewData["CategoryId"] = new SelectList(_dbcontext.Categories.Where(c => c.IsDelete == false).Where(s => s.IsDisplay == true), "CategoryId", "CatergoryName", productDto.CategoryId);
                ViewData["SupplierId"] = new SelectList(_dbcontext.Suppliers.Where(s => s.IsDelete == false).Where(s => s.IsDisplay == true), "SupplierId", "SupplierName", productDto.SupplierId);
                ViewData["ManufacturerId"] = new SelectList(_dbcontext.Manufacturers.Where(s => s.IsDelete == false).Where(s => s.IsDisplay == true), "ManufacturerId", "ManufacturerName", productDto.ManufacturerId);
                return View(productDto);
            }
        }

        [HttpPost]
        public IActionResult Update(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                _productService.UpdateProduct(productDto);
                return RedirectToAction("Index");
            }
            ViewData["CategoryId"] = new SelectList(_dbcontext.Categories.Where(c => c.IsDelete == false).Where(s => s.IsDisplay == true), "CategoryId", "CatergoryName", productDto.CategoryId);
            ViewData["SupplierId"] = new SelectList(_dbcontext.Suppliers.Where(s => s.IsDelete == false).Where(s => s.IsDisplay == true), "SupplierId", "SupplierName", productDto.SupplierId);
            ViewData["ManufacturerId"] = new SelectList(_dbcontext.Manufacturers.Where(s => s.IsDelete == false).Where(s => s.IsDisplay == true), "ManufacturerId", "ManufacturerName", productDto.ManufacturerId);
            return View();
        }

        public async Task<IActionResult> Details(long id)
        {
            if (SessionHelper.GetSession<User>(HttpContext.Session, "Login") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {

                var product = from p in _dbcontext.Products
                                     join i in _dbcontext.Images on p.ProductId equals i.ProductId
                                     join c in _dbcontext.Categories on p.CategoryId equals c.CategoryId
                                     join m in _dbcontext.Manufacturers on p.ManufacturerId equals m.ManufacturerId
                                     join s in _dbcontext.Suppliers on p.SupplierId equals s.SupplierId
                                     where p.IsDisplay == true && p.IsDelete == false
                                     where i.IsDelete == false && i.IsDisplay == true
                                     where p.ProductId == id
                                     select new ProductDetailViewModel()
                                     {
                                        ProductId= p.ProductId, 
                                        ProductName= p.ProductName, 
                                         Ram = p.Ram,
                                         Rom = p.Rom,
                                         Screen = p.Screen,
                                         Size = p.Size,
                                         SupplierName =s.SupplierName,
                                         CategoryName = c.CatergoryName,
                                         ManufacturerName = m.ManufacturerName,
                                         Support = p.Support,
                                         UpdateDate= p.UpdateDate,
                                         InStock= p.InStock,
                                         GPU =p.GPU, 
                                         CameraFirst = p.CameraFirst,
                                         CameraLast = p.CameraLast,
                                         Description = p.Description,
                                         UrlImg = i.Url,OperatingSystem = p.OperatingSystem,
                                         CostSell= p.CostSell,
                                         CostBuy = p.CostBuy,
                                         CreateDate = p.CreateDate

                                     };
                return View(product);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (SessionHelper.GetSession<User>(HttpContext.Session, "Login") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var product = await _productService.GetByIdAsync(id);
                if (product != null)
                {
                    _productService.DeleteProduct(id);
                    return RedirectToAction("Index");
                }
                return StatusCode(404);
            }
        }

    }
}
