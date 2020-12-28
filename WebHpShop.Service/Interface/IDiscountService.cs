using System;
using System.Collections.Generic;
using System.Text;
using WebHpShop.Domain.Entities;
using WebHpShop.Reponsitory.Dto;
using WebHpShop.Service.Interfaces;

namespace WebHpShop.Service.Interface
{
    public interface IDiscountService : IServices<Discount>
    {
        Discount Create(DiscountDto discountDto);
        void UpdateDiscount(DiscountDto discountDto);
        void DeleteDiscount(long Id);
    }
}
