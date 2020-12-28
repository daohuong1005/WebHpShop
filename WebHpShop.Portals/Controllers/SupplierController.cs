using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebHpShop.Service.Interface;
using WebHpShop.Service.ViewModel;
using WebStore.Domain;
using System.Linq.Dynamic.Core;
using WebHpShop.Reponsitory.Dto;
using WebHpShop.Portals.Hepler;
using WebHpShop.Domain.Entities;

namespace WebHpShop.Portals.Controllers
{
    public class SupplierController : Controller
    {
        private ISupplierService _supplierService;
        private readonly IMapper _mapper;
        private readonly WebHpShopDbContext _dbcontext;

        public SupplierController(ISupplierService supplierService,  IMapper mapper, WebHpShopDbContext dbcontext)
        {
            _supplierService = supplierService;
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
                var supplier = await _supplierService.GetAllAsync();
                var supplierViewModel = _mapper.Map<IList<SupplierViewModel>>(supplier);
                return View(supplierViewModel);
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


                var supplierData = (from p in _dbcontext.Suppliers
                                    where p.IsDisplay == true
                                    where p.IsDelete == false
                                select new { p.SupplierId,p.SupplierName,p.SupplierCode,p.Email });

                //Sort data
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    supplierData = supplierData.OrderBy(sortColumn + " " + sortColumnDirection);
                    //(sortColumn + " " + sortColumnDirection);
                }
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    supplierData = supplierData.Where(m => m.SupplierName.Contains(searchValue));
                }

                //total number of rows count   
                recordsTotal = supplierData.Count();

                //Paging   
                var data = supplierData.Skip(skip).Take(pageSize).ToList();

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
                return View();
            }
        }
        [HttpPost]
        public IActionResult Create(SupplierDto supplierDto)
        {
            if(ModelState.IsValid)
            {
                var reulst = _supplierService.Create(supplierDto);
                return RedirectToAction("Index");
            }    
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
                var supplier = await _supplierService.GetByIdAsync(id);
                var supplierDto = _mapper.Map<SupplierDto>(supplier);
                return View(supplierDto);
            }
        }

        [HttpPost]
        public IActionResult Update(SupplierDto supplierDto)
        {
            _supplierService.UpdateSupplier(supplierDto);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(long Id)
        {
            if (SessionHelper.GetSession<User>(HttpContext.Session, "Login") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                try
                {
                    var supplier = await _supplierService.GetByIdAsync(Id);
                    if (supplier != null)
                    {
                        _supplierService.Delete(Id);
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
