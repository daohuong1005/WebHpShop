using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using WebHpShop.Domain.Entities;
using WebHpShop.Reponsitory.Dto;
using WebStore.Domain;
using System.Linq.Dynamic.Core;
using AutoMapper;
using WebHpShop.Portals.Hepler;

namespace WebHpShop.Portals.Controllers
{
    public class ImageController : Controller
    {
        private readonly WebHpShopDbContext _dbcontext;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IMapper _mapper;

        public ImageController(WebHpShopDbContext context,IMapper mapper, IWebHostEnvironment hostEnvironment)
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
                return View(await _dbcontext.Images.ToListAsync());
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


                var imageData = (from c in _dbcontext.Images
                                 join p in _dbcontext.Products on c.ProductId equals p.ProductId
                                 where c.IsDisplay == true
                                 select new { c.ImageId, c.Url,p.ProductName,c.IsDisplay });

                //Sort data
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    imageData = imageData.OrderBy(sortColumn + " " + sortColumnDirection);
                    //(sortColumn + " " + sortColumnDirection);
                }
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    imageData = imageData.Where(m => m.ProductName.Contains(searchValue));
                }

                //total number of rows count   
                recordsTotal = imageData.Count();

                //Paging   
                var data = imageData.Skip(skip).Take(pageSize).ToList();

                //Returning Json Data  
                var json = Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
                return json;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string UploadedFile(ImageDto imageDto)
        {
            string newFileName = null;

            if (imageDto.PathImage != null)
            {
                var fileName = Path.GetFileName(imageDto.PathImage.FileName);
                var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                var fileExtension = Path.GetExtension(fileName);
                newFileName = String.Concat(myUniqueFileName, fileExtension);
                var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images")).Root + $@"\{newFileName}";
                using (FileStream fs = System.IO.File.Create(filepath))
                {
                    imageDto.PathImage.CopyTo(fs);
                    fs.Flush();
                }
            }
            return newFileName;
        }

        public IActionResult Create()
        {
            if (SessionHelper.GetSession<User>(HttpContext.Session, "Login") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                ViewData["ProductId"] = new SelectList(_dbcontext.Products.Where(c => c.IsDelete == false).Where(s => s.IsDisplay == true), "ProductId", "ProductName");
                return View();
            }
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(ImageDto imageDto)
        {
           
                if (ModelState.IsValid)
                {
                    Image images = new Image()
                    {
                        ImageId = imageDto.ImageId,
                        Url = UploadedFile(imageDto),
                        ProductId = imageDto.ProductId,
                        IsDelete = imageDto.IsDelete,
                        IsDisplay = imageDto.IsDisplay
                    };
                    _dbcontext.Add(images);
                    await _dbcontext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(imageDto);  
        }

        public async Task<IActionResult> Update(long? id)
        {
            if (SessionHelper.GetSession<User>(HttpContext.Session, "Login") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }

                var image = await _dbcontext.Images.FindAsync(id);
                var imageDto = _mapper.Map<ImageDto>(image);
                if (image == null)
                {
                    return NotFound();
                }
                ViewData["ProductId"] = new SelectList(_dbcontext.Products.Where(p => p.IsDelete == false)
                                                                          .Where(p => p.IsDisplay == true), "ProductId", "ProductName", image.ProductId);
                return View(imageDto);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(ImageDto imageDto)
        {
            if (ModelState.IsValid)
            {
                    Image images = new Image()
                    {
                        ImageId = imageDto.ImageId,
                        Url = UploadedFile(imageDto),
                        ProductId = imageDto.ProductId,
                        IsDelete = imageDto.IsDelete,
                        IsDisplay = imageDto.IsDisplay
                    };
                    _dbcontext.Images.Update(images);
                    await _dbcontext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_dbcontext.Products.Where(p => p.IsDelete == false)
                                                                                  .Where(p => p.IsDisplay == true), "ProductId", "ProductName", imageDto.ProductId);
            return View(imageDto);
        }

        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (SessionHelper.GetSession<User>(HttpContext.Session, "Login") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var img = await _dbcontext.Images.FindAsync(id);
                _dbcontext.Images.Remove(img);
                await _dbcontext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
