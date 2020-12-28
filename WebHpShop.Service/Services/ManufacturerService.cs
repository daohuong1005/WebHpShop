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
    public class ManufacturerService : BaseService<Manufacturer>, IManufacturerService
    {
        private readonly IManufacturerReponsitory _manufacturerReponsitory;
        private readonly IMapper _mapper;
        public ManufacturerService(IManufacturerReponsitory manufacturerReponsitory, IMapper mapper) : base(manufacturerReponsitory)
        {
            _manufacturerReponsitory = manufacturerReponsitory;
            _mapper = mapper;
        }

        public void DeleteManufacturer(long Id)
        {
            
            _manufacturerReponsitory.DeleteManufacturer(Id);
        }

        public Manufacturer Create(ManufacturerDto manufacturerDto)
        {
            var manufacturer = _mapper.Map<Manufacturer>(manufacturerDto);
            return _manufacturerReponsitory.Create(manufacturer);
        }


        public void UpdateManufacturer(ManufacturerDto manufacturerDto)
        {
            var manufacturer = _mapper.Map<Manufacturer>(manufacturerDto);
            _manufacturerReponsitory.UpdateManufacturer(manufacturer);
        }
        public async Task<IEnumerable<ManufacturerViewModel>> GetAllManufacturer()
        {
            var manufacturers = await _manufacturerReponsitory.GetAllManufacturer();
            return _mapper.Map<IEnumerable<ManufacturerViewModel>>(manufacturers);
        }
    }
}
