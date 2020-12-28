using System;
using System.Collections.Generic;
using System.Text;

namespace WebHpShop.Service.ViewModel
{
    public class OrderViewModel
    {
        //p.OrderId,p.OrderDate,p.ShipDate,p.StatusId,p.UserId,p.Total,u.Username,u.FirstName,
        //                          u.LastName,u.Phone,u.Address,u.Gender,u.Birthday
        public long OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public long StatusId { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string FirsName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool Gender { get; set; }
        public string NameStt { get; set; }
        public double Total { get; set; }
        public DateTime Bithday { get; set; }
        public string OrderAdress { get; set; }

    }
}
