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
    public class ManufacturerController : Controller
    {
        private IManufacturerService _manufacturerService;
        private readonly IMapper _mapper;
        private readonly WebHpShopDbContext _dbcontext;

        public ManufacturerController(IManufacturerService manufacturerService, IMapper mapper, WebHpShopDbContext dbcontext)
        {
            _manufacturerService = manufacturerService;
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
                var manufacturer = await _manufacturerService.GetAllAsync();
                var manufacturerViewModel = _mapper.Map<IList<ManufacturerViewModel>>(manufacturer);
                return View(manufacturerViewModel);
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


                var manufacturerData = (from p in _dbcontext.Manufacturers
                                        where p.IsDelete == false
                                        where p.IsDisplay == true
                                        select new { p.ManufacturerId, p.ManufacturerName });

                //Sort data
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    manufacturerData = manufacturerData.OrderBy(sortColumn + " " + sortColumnDirection);
                    //(sortColumn + " " + sortColumnDirection);
                }
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    manufacturerData = manufacturerData.Where(m => m.ManufacturerName.Contains(searchValue));
                }

                //total number of rows count   
                recordsTotal = manufacturerData.Count();

                //Paging   
                var data = manufacturerData.Skip(skip).Take(pageSize).ToList();

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
        public IActionResult Create(ManufacturerDto manufacturerDto)
        {
            if (ModelState.IsValid)
            {
                var reulst = _manufacturerService.Create(manufacturerDto);
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
                var manufacturer = await _manufacturerService.GetByIdAsync(id);
                var manufacturerDto = _mapper.Map<ManufacturerDto>(manufacturer);
                return View(manufacturerDto);
            }
        }

        [HttpPost]
        public IActionResult Update(ManufacturerDto manufacturerDto)
        {
            if (ModelState.IsValid)
            {
                _manufacturerService.UpdateManufacturer(manufacturerDto);
                return RedirectToAction("Index");
            }
            return View();
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
                var manufacturer = await _manufacturerService.GetByIdAsync(id);
                if (manufacturer != null)
                {
                    _manufacturerService.DeleteManufacturer(id);
                    return RedirectToAction("Index");
                }
                return StatusCode(404);
            }
        }


        [HttpGet]
        public async Task<ActionResult> GetAllManufacturer()
        {
            try
            {
                var manufacturers = await _manufacturerService.GetAllManufacturer();
                var result = _mapper.Map<IEnumerable<ManufacturerViewModel>>(manufacturers);
                return View(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

    }
}
