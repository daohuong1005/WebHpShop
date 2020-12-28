using System;
using System.Collections.Generic;
using System.Text;
using WebHpShop.Domain.Entities;
using WebHpShop.Reponsitory.Dto;
using WebHpShop.Service.Interfaces;

namespace WebHpShop.Service.Interface
{
    public interface ISupplierService : IServices<Supplier>
    {
        Supplier Create(SupplierDto supplier);
        void UpdateSupplier(SupplierDto supplier);
        void Delete(long id);
    }
}
