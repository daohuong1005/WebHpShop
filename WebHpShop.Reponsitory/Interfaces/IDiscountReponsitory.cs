using System;
using System.Collections.Generic;
using System.Text;
using WebHpShop.Domain.Entities;

namespace WebHpShop.Reponsitory.Interfaces
{
    public interface IDiscountReponsitory : IReponsitory<Discount>
    {
        Discount Create(Discount discount);
        void UpdateDiscount(Discount discount);
        void DeleteDiscount(long Id);
    }
}
