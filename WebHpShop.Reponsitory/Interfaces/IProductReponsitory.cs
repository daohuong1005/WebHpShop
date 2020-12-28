using System;
using System.Collections.Generic;
using System.Text;
using WebHpShop.Domain.Entities;

namespace WebHpShop.Reponsitory.Interfaces
{
    public interface IProductReponsitory : IReponsitory<Product>
    {
        Product Create(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(long Id);
    }
}
