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
    public interface IManufacturerService : IServices<Manufacturer>
    {
        Manufacturer Create(ManufacturerDto manufacturerDto);
        void UpdateManufacturer(ManufacturerDto manufacturerDto);
        void DeleteManufacturer(long Id);

        Task<IEnumerable<ManufacturerViewModel>> GetAllManufacturer();
    }
}
