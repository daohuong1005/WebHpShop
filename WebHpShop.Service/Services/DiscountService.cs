using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WebHpShop.Domain.Entities;
using WebHpShop.Reponsitory.Dto;
using WebHpShop.Reponsitory.Interfaces;
using WebHpShop.Service.Interface;

namespace WebHpShop.Service.Services
{
    public class DiscountService : BaseService<Discount>, IDiscountService
    {
        private readonly IDiscountReponsitory _discountReponsitory;
        private readonly IMapper _mapper;
        public DiscountService(IDiscountReponsitory discountReponsitory, IMapper mapper) : base(discountReponsitory)
        {
            _discountReponsitory = discountReponsitory;
            _mapper = mapper;
        }

        public void DeleteDiscount(long Id)
        {
            
            _discountReponsitory.DeleteDiscount(Id);
        }

        public Discount Create(DiscountDto discountDto)
        {
            var discount = _mapper.Map<Discount>(discountDto);
            return _discountReponsitory.Create(discount);
        }


        public void UpdateDiscount(DiscountDto discountDto)
        {
            var discount = _mapper.Map<Discount>(discountDto);
            _discountReponsitory.UpdateDiscount(discount);
        }
    }
}
