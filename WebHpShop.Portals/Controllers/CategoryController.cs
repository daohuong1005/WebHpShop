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
using Microsoft.AspNetCore.Mvc.Rendering;
using WebHpShop.Domain.Entities;
using WebHpShop.Portals.Hepler;

namespace WebHpShop.Portals.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly WebHpShopDbContext _dbcontext;

        public CategoryController(ICategoryService categoryService, IMapper mapper, WebHpShopDbContext dbcontext)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _dbcontext = dbcontext;
        }
        public async Task<IActionResult> Index()
        {
            if (SessionHelper.GetSession<User>(HttpContext.Session, "Login") == null)
            {
                return RedirectToAction("Login","User");
            }
            else
            {
                var categories = await _categoryService.GetAllAsync();
                var categoryViewModel = _mapper.Map<IList<CategoryViewModel>>(categories);
                return View(categoryViewModel);
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


                var categoryData = (from c in _dbcontext.Categories
                                    where c.IsDelete == false
                                    where c.IsDisplay == true
                                    select new { c.CategoryId, c.CatergoryName });

                //Sort data
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    categoryData = categoryData.OrderBy(sortColumn + " " + sortColumnDirection);
                    //(sortColumn + " " + sortColumnDirection);
                }
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    categoryData = categoryData.Where(m => m.CatergoryName.Contains(searchValue));
                }

                //total number of rows count   
                recordsTotal = categoryData.Count();

                //Paging   
                var data = categoryData.Skip(skip).Take(pageSize).ToList();

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
        public IActionResult Create(CategoryDto categoryDto)
        {
            if (ModelState.IsValid)
            {
                var reulst = _categoryService.Create(categoryDto);
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
                var category = await _categoryService.GetByIdAsync(id);
                var categoryDto = _mapper.Map<CategoryDto>(category);
                return View(categoryDto);
            }
        }

        [HttpPost]
        public IActionResult Update(CategoryDto categoryDto)
        {
            if (ModelState.IsValid)
            {
                _categoryService.UpdateCategory(categoryDto);
                return RedirectToAction("Index");
            }
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
                var category = await _categoryService.GetByIdAsync(id);
            if (category != null)
            {
                _categoryService.DeleteCategory(id);
                return RedirectToAction("Index");
            }
            return StatusCode(404);
             }
        }

        [HttpGet]
        public async Task<ActionResult> GetAllCategory()
        {
            try
            {
                var categories = await _categoryService.GetAllCategory();
                var result = _mapper.Map<IEnumerable<CategoryViewModel>>(categories);
                return View(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }



    }
}
