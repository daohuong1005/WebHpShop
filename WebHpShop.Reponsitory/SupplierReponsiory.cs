using System;
using System.Collections.Generic;
using System.Text;
using WebHpShop.Domain.Entities;
using WebHpShop.Reponsitory.Interfaces;
using WebStore.Domain;

namespace WebHpShop.Reponsitory
{
    public class SupplierReponsiory : BaseReponsitory<Supplier>, ISupplierReponsitory
    {
        public SupplierReponsiory(WebHpShopDbContext dbcontext) : base(dbcontext)
        {

        }

        public Supplier Create(Supplier supplier)
        {
            DbContext.Suppliers.Add(supplier);
            DbContext.SaveChanges();
            return supplier;
        }

        public void Delete(long id)
        {
            var supplier = DbContext.Suppliers.Find(id);
            if (supplier != null)
            {
                supplier.IsDelete = true;
                supplier.IsDisplay = false;
                DbContext.Suppliers.Update(supplier);
                DbContext.SaveChanges();
            }

        }

        public void UpdateSupplier(Supplier supplier)
        {
                supplier.UpdateDate = DateTime.Now;
                DbContext.Suppliers.Update(supplier);
                DbContext.SaveChanges();
            
        }
    }
}
