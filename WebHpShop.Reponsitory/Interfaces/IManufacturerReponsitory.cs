using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebHpShop.Domain.Entities;

namespace WebHpShop.Reponsitory.Interfaces
{
    public interface IManufacturerReponsitory : IReponsitory<Manufacturer>
    {
        Manufacturer Create(Manufacturer manufacturer);
        void UpdateManufacturer(Manufacturer manufacturer);
        void DeleteManufacturer(long Id);

        Task<IEnumerable<Manufacturer>> GetAllManufacturer();
    }
}
