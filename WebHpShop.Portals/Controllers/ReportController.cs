using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using System.Linq.Dynamic.Core;
using WebHpShop.Portals.Hepler;
using WebHpShop.Domain.Entities;
using ClosedXML.Excel;
using System.IO;

namespace WebHpShop.Portals.Controllers
{
    public class ReportController : Controller
    {
        private readonly WebHpShopDbContext _dbcontext;

        public ReportController(WebHpShopDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        public IActionResult Index()
        {
            if (SessionHelper.GetSession<User>(HttpContext.Session, "Login") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
                return View();
        }
        public IActionResult Reports()
        {
            return View();
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
                                 where o.StatusId == 4 && o.IsDelete == false
                                 select new { o.OrderId, u.Username, o.OrderDate, s.StatusName, o.Total });



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

        public IActionResult ExportToExcel()
        {

            string starts = Request.Form["start"];
            string ends = Request.Form["end"];
            DateTime start;
            DateTime end;
            if (starts == "" && ends == "")
            {
                start = DateTime.MinValue;
                end = DateTime.Now;
            }
            else if (starts == "" && ends != "")
            {
                start = DateTime.MinValue;
                end = DateTime.Parse(Request?.Form["end"]);
            }
            else if (starts != "" && ends == "")
            {
                start = DateTime.Parse(Request?.Form["start"]);
                end = DateTime.Now;
            }
            else
            {
                start = DateTime.Parse(Request?.Form["start"]);
                end = DateTime.Parse(Request?.Form["end"]);
            }
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "BaoCao.xlsx";
            var orderData = (from o in _dbcontext.Orders
                             join u in _dbcontext.Users on o.UserId equals u.UserId
                             join s in _dbcontext.Statuses on o.StatusId equals s.Id
                             where o.StatusId == 4 && o.IsDelete == false
                             where start < o.OrderDate && o.OrderDate < end
                             select new { o.OrderId, u.Username,u.LastName,u.FirstName, u.Phone, o.OrderDate, o.ShipDate, s.StatusName, o.Total, o.OrderAddress });
            if (start == DateTime.MinValue && end == DateTime.MinValue || end == DateTime.MinValue)
            {
                 orderData = (from o in _dbcontext.Orders
                                 join u in _dbcontext.Users on o.UserId equals u.UserId
                                 join s in _dbcontext.Statuses on o.StatusId equals s.Id
                                 where o.StatusId == 4 && o.IsDelete == false
                                 where start < o.OrderDate && o.OrderDate < DateTime.Now
                                 select new { o.OrderId, u.Username, u.LastName, u.FirstName, u.Phone, o.OrderDate,o.ShipDate, s.StatusName, o.Total,o.OrderAddress });
            }

            double tongtien = 0;
            foreach (var item in orderData)
            {
                tongtien += item.Total;
            }
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet = workbook.Worksheets.Add("Orders");
                    worksheet.Style.Font.SetFontName("Times New Roman");
                    worksheet.Style.Font.SetFontSize(12);
                    worksheet.Cell(1, 1).Value = "STT";
                    worksheet.Cell(1, 2).Value = "Mã Đơn Hàng";
                    worksheet.Cell(1, 3).Value = "Họ và tên";
                    worksheet.Cell(1, 4).Value = "Số điện thoại";
                    worksheet.Cell(1, 5).Value = "Địa chỉ";
                    worksheet.Cell(1, 6).Value = "Ngày đặt hàng";
                    worksheet.Cell(1, 7).Value = "Ngày giao hàng";
                    worksheet.Cell(1, 8).Value = "Trạng thái";
                    worksheet.Cell(1, 10).Value = "Tổng tiền";
                    int i = 1;
                    foreach (var item in orderData)
                    {
                        worksheet.Cell(i + 1, 1).Value = i;
                        worksheet.Cell(i + 1, 2).Value = item.OrderId;
                        worksheet.Cell(i + 1, 3).Value = (item.FirstName + " " + item.LastName);
                        worksheet.Cell(i + 1, 4).Value = "(+84)" + item.Phone;
                        worksheet.Cell(i + 1, 5).Value = item.OrderAddress;
                        worksheet.Cell(i + 1, 6).Value = item.OrderDate;
                        worksheet.Cell(i + 1, 7).Value = item.ShipDate;
                        worksheet.Cell(i + 1, 8).Value = item.StatusName;
                        worksheet.Cell(i + 1, 10).Value = item.Total.ToString("#,###") + " đ";
                        i++;
                    }
                    worksheet.Cell(i + 1, 9).Value = "Doanh thu";
                    worksheet.Cell(i + 1, 10).Value = tongtien.ToString("#,###") + " đ";
                    worksheet.Cell(i + 1, 10).Style.Font.SetFontColor(XLColor.Red);
                    SetHeader(ref worksheet, i + 1, 9);
                    for (i = 1; i <= 10; i++)
                    {
                        SetHeader(ref worksheet, 1, i);
                    }
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        RedirectToAction("Index", "Report");
                        return File(content, contentType, fileName);
                    }
                    
                }
                
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public void SetHeader(ref IXLWorksheet worksheet, int i, int j)
        {
            worksheet.Cell(i, j).Style.Fill.SetBackgroundColor(XLColor.Blue);
            worksheet.Cell(i, j).Style.Font.SetFontColor(XLColor.White);
            worksheet.Cell(i, j).Style.Font.Bold = true;
            worksheet.Cell(i, i).Style.Font.SetFontSize(13);
            worksheet.Cell(i, j).Style.Font.SetFontName("Times New Roman");
        }

    }
}
