using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebHpShop.Domain.Entities;

namespace WebHpShop.Reponsitory.Interfaces
{
    public interface ICategoryReponsitory : IReponsitory<Category>
    {
        Category Create(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(long Id);

        Task<IEnumerable<Category>> GetAllCategory();
    }
}
