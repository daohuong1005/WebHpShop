using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebHpShop.Domain.Entities;
using WebHpShop.Reponsitory.Dto;
using WebHpShop.Reponsitory.Interfaces;
using WebHpShop.Service.Interface;
using WebHpShop.Service.ViewModel;

namespace WebHpShop.Service.Services
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        private readonly ICategoryReponsitory _categoryReponsitory;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryReponsitory categoryReponsitory, IMapper mapper) : base(categoryReponsitory)
        {
            _categoryReponsitory = categoryReponsitory;
            _mapper = mapper;
        }

        public void DeleteCategory(long Id)
        {
           
            _categoryReponsitory.DeleteCategory(Id);
        }

        public Category Create(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            return _categoryReponsitory.Create(category);
        }


        public void UpdateCategory(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            _categoryReponsitory.UpdateCategory(category);
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllCategory()
        {
            var category = await _categoryReponsitory.GetAllCategory();
            return _mapper.Map<IEnumerable<CategoryViewModel>>(category);
        }
    }
}
