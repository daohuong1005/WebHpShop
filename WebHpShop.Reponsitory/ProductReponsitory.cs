using System;
using System.Collections.Generic;
using System.Text;
using WebHpShop.Domain.Entities;
using WebHpShop.Reponsitory.Interfaces;
using WebStore.Domain;

namespace WebHpShop.Reponsitory
{
    public class ProductReponsitory : BaseReponsitory<Product>, IProductReponsitory
    {
        public ProductReponsitory(WebHpShopDbContext dbcontext) : base(dbcontext)
        {

        }

        public Product Create(Product product)
        {
            DbContext.Products.Add(product);
            DbContext.SaveChanges();
            return product;
        }


        public void UpdateProduct(Product product)
        {

            product.UpdateDate = DateTime.Now;
            DbContext.Products.Update(product);
            DbContext.SaveChanges();
        }

        public void DeleteProduct(long Id )
        {

            var product = DbContext.Products.Find(Id);
            if (product != null)
            {
                product.IsDelete = true;
                product.IsDisplay = false;
                DbContext.Products.Update(product);
                DbContext.SaveChanges();
            }
        }
    }
}
