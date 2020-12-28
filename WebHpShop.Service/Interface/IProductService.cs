using System;
using System.Collections.Generic;
using System.Text;
using WebHpShop.Domain.Entities;
using WebHpShop.Reponsitory.Dto;
using WebHpShop.Service.Interfaces;

namespace WebHpShop.Service.Interface
{
    public interface IProductService : IServices<Product>
    {
        Product Create(ProductDto productDto);
        void UpdateProduct(ProductDto productDto);
        void DeleteProduct(long Id);
    }
}
