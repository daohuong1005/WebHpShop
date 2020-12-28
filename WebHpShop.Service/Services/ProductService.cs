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
    public class ProductService : BaseService<Product>, IProductService
    {
        private readonly IProductReponsitory _productReponsitory;
        private readonly IMapper _mapper;
        public ProductService(IProductReponsitory productReponsitory, IMapper mapper) : base(productReponsitory)
        {
            _productReponsitory = productReponsitory;
            _mapper = mapper;
        }

        public void DeleteProduct(long Id)
        {
           
            _productReponsitory.DeleteProduct(Id);
        }

        public Product Create(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            return _productReponsitory.Create(product);
        }


        public void UpdateProduct(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            _productReponsitory.UpdateProduct(product);
        }
    }
}
