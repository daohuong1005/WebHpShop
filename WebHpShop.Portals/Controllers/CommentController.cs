using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebHpShop.Domain.Entities;
using WebHpShop.Portals.Hepler;
using WebHpShop.Reponsitory.Dto;
using WebHpShop.Service.ViewModel;
using WebStore.Domain;

namespace WebHpShop.Portals.Controllers
{
    public class CommentController : Controller
    {
        private readonly WebHpShopDbContext _dbcontext;
        public CommentController(WebHpShopDbContext dbContext)
        {
            _dbcontext = dbContext;
        }    
        public async Task<IActionResult> Index()
        {
            if (SessionHelper.GetSession<User>(HttpContext.Session, "Login") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                return View(await _dbcontext.Comments.ToListAsync());
            }
           
        }

        public IActionResult GetAllComment(long id)
        {
            var listComments = (from c in _dbcontext.Comments
                                join p in _dbcontext.Products on c.ProductId equals p.ProductId
                                join u in _dbcontext.Users on c.UserId equals u.UserId
                                orderby c.CommentId descending
                                where p.ProductId == id && c.IsDelete == false && c.IsDisplay == true
                                select new CommentViewModel
                                {
                                    CommentId = c.CommentId,
                                    ProductId = c.ProductId,
                                    NameUser = u.Username,
                                    Content = c.Content,
                                    DateCreate = c.CreateDate
                                }
                                ).ToList();

            return View(listComments);
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
                //var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();

                // Search Value from (Search box)  
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                //Paging Size (10,20,50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                // Getting all supplier data 


                var listComments = (from c in _dbcontext.Comments
                                   join p in _dbcontext.Products on c.ProductId equals p.ProductId
                                   join u in _dbcontext.Users on c.UserId equals u.UserId
                                   orderby c.CommentId descending
                                   where c.IsDelete == false && c.IsDisplay == true
                                   select new                                    {
                                       c.CommentId,
                                        p.ProductName,
                                       u.Username,
                                       c.Content,
                                       c.CreateDate
                                   }
                                );

                //Sort data
                //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                //{
                //    listComments = listComments.OrderBy(sortColumn + " " + sortColumnDirection);
                //    //(sortColumn + " " + sortColumnDirection);
                //}
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    listComments = listComments.Where(m => m.Username.Contains(searchValue));
                }

                //total number of rows count   
                recordsTotal = listComments.Count();

                //Paging   
                var data = listComments.Skip(skip).Take(pageSize).ToList();

                //Returning Json Data  
                var json = Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
                return json;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public IActionResult Create()
        {

            long productid = long.Parse(Request.Form["productId"]);
            string content = Request.Form["contentCmt"];
            long userId = long.Parse(Request.Form["userId"]);
            if (ModelState.IsValid)
            {
                Comment cmt = new Comment()
                {
                    Content = content,
                    ProductId = productid,
                    UserId = userId
                };    
                _dbcontext.Comments.Add(cmt);
                _dbcontext.SaveChanges();
                return RedirectToAction("ProductDetail", "Product",new { @id = productid });
            }
            return View();
        }
        public IActionResult Delete(long id)
        {
                var cmt = _dbcontext.Comments.Find(id);
                _dbcontext.Comments.Remove(cmt);
                _dbcontext.SaveChangesAsync();
                return RedirectToAction("GetAllComment", "Comment");
        }


        public IActionResult DeleteAdmin(long id)
        {
            var cmt = _dbcontext.Comments.Find(id);
            _dbcontext.Comments.Remove(cmt);
            _dbcontext.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
