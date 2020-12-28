using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WebHpShop.Domain.Entities;
using WebHpShop.Reponsitory.Interfaces;
using WebHpShop.Reponsitory.Dto;
using WebHpShop.Service.Interface;

namespace WebHpShop.Service.Services
{
    public class SupplierService : BaseService<Supplier>, ISupplierService
    {
        private readonly ISupplierReponsitory _supplierReponsitory;
        private readonly IMapper _mapper;
        public SupplierService(ISupplierReponsitory supplierReponsitory, IMapper mapper) : base(supplierReponsitory)
        {
            _supplierReponsitory = supplierReponsitory;
            _mapper = mapper;
        }

        public Supplier Create(SupplierDto supplierdto)
        {
            var suppliers = _mapper.Map<Supplier>(supplierdto);
            return _supplierReponsitory.Create(suppliers);
        }

        public void Delete(long id)
        {
            _supplierReponsitory.Delete(id);
        }

        public void UpdateSupplier(SupplierDto supplierdto)
        {
            var suppliers = _mapper.Map<Supplier>(supplierdto);
            _supplierReponsitory.UpdateSupplier(suppliers);
        }
    }
}
