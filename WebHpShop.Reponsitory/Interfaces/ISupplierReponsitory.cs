using System;
using System.Collections.Generic;
using System.Text;
using WebHpShop.Domain.Entities;

namespace WebHpShop.Reponsitory.Interfaces
{
    public interface ISupplierReponsitory : IReponsitory<Supplier>
    {
        Supplier Create(Supplier supplier);
        void UpdateSupplier(Supplier supplier);
        void Delete(long id);
    }
}
