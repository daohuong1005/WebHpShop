using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebHpShop.Domain.Entities;
using WebHpShop.Reponsitory.Dto;
using WebHpShop.Service.Interfaces;
using WebHpShop.Service.ViewModel;

namespace WebHpShop.Service.Interface
{
    public interface ICategoryService : IServices<Category>
    {
        Category Create(CategoryDto categoryDto);
        void UpdateCategory(CategoryDto categoryDto);
        void DeleteCategory(long Id);
        Task<IEnumerable<CategoryViewModel>> GetAllCategory();
    }
}
