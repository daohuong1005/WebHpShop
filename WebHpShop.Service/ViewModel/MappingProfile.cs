using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WebHpShop.Domain.Entities;
using WebHpShop.Reponsitory.Dto;


namespace WebHpShop.Service.ViewModel
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MappingEntityToViewModel();
            MappingDtoToEntity();
        }

        private void MappingEntityToViewModel()
        {

            CreateMap<User, UserViewModel>();
            CreateMap<UserViewModel, User>();

            CreateMap<SupplierViewModel, Supplier>();
            CreateMap<Supplier, SupplierViewModel>();

            CreateMap<ManufacturerViewModel, Manufacturer>();
            CreateMap<Manufacturer, ManufacturerViewModel>();

            CreateMap<CategoryViewModel, Category>();
            CreateMap<Category, CategoryViewModel>();

            CreateMap<ProductViewModel, Product>();
            CreateMap<Product, ProductViewModel>();

            CreateMap<ImageViewModel, Image>();
            CreateMap<Image, ImageViewModel>();

            CreateMap<DiscountViewModel, Discount>();
            CreateMap<Discount, DiscountViewModel>();

        }

        private void MappingDtoToEntity()
        {

            CreateMap<LoginDto, User>();
            CreateMap<User, LoginDto>();

            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();

            CreateMap<SupplierDto, Supplier>();
            CreateMap<Supplier, SupplierDto>();

            CreateMap<Manufacturer, ManufacturerDto>();
            CreateMap<ManufacturerDto, Manufacturer>();

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            CreateMap<Discount, DiscountDto>();
            CreateMap<DiscountDto, Discount>();

            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();

            CreateMap<Image, ImageDto>();
            CreateMap<ImageDto, Image>();

        }
    }
}
