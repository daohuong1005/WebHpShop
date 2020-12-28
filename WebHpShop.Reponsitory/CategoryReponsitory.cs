using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebHpShop.Domain.Entities;
using WebHpShop.Reponsitory.Interfaces;
using WebStore.Domain;

namespace WebHpShop.Reponsitory
{
    public class CategoryReponsitory : BaseReponsitory<Category>, ICategoryReponsitory
    {
        public CategoryReponsitory(WebHpShopDbContext dbcontext) : base(dbcontext)
        {

        }

        public Category Create(Category category)
        {
            DbContext.Categories.Add(category);
            DbContext.SaveChanges();
            return category;
        }


        public void UpdateCategory(Category category)
        {

            category.UpdateDate = DateTime.Now;
            DbContext.Categories.Update(category);
            DbContext.SaveChanges();
        }

        public void DeleteCategory(long Id)
        {
            var category = DbContext.Categories.Find(Id);
             if(category != null)
            {
                category.IsDelete = true;
                category.IsDisplay = false;
                DbContext.Categories.Update(category);
                DbContext.SaveChanges();
            }    
            
        }

        public async Task<IEnumerable<Category>> GetAllCategory()
        {
            var categoryChild = await DbContext.Categories
                .Where(c => c.IsDelete.Equals(false) && c.IsDisplay.Equals(true)).ToListAsync();
            return categoryChild;
        }
    }
}
