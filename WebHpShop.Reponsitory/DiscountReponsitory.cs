using System;
using System.Collections.Generic;
using System.Text;
using WebHpShop.Domain.Entities;
using WebHpShop.Reponsitory.Interfaces;
using WebStore.Domain;

namespace WebHpShop.Reponsitory
{
    public class DiscountReponsitory : BaseReponsitory<Discount>, IDiscountReponsitory
    {
        public DiscountReponsitory(WebHpShopDbContext dbcontext) : base(dbcontext)
        {

        }

        public Discount Create(Discount discount)
        {
            DbContext.Discounts.Add(discount);
            DbContext.SaveChanges();
            return discount;
        }


        public void UpdateDiscount(Discount discount)
        {

            discount.UpdateDate = DateTime.Now;
            DbContext.Discounts.Update(discount);
            DbContext.SaveChanges();
        }

        public void DeleteDiscount(long Id )
        {

            var discount = DbContext.Discounts.Find(Id);
            if (discount != null)
            {
                discount.IsDelete = true;
                discount.IsDisplay = false;
                DbContext.Discounts.Update(discount);
                DbContext.SaveChanges();
            }
        }
    }
}
