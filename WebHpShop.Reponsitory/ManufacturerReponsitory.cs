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
    public class ManufacturerReponsitory : BaseReponsitory<Manufacturer>, IManufacturerReponsitory
    {
        public ManufacturerReponsitory(WebHpShopDbContext dbcontext) : base(dbcontext)
        {

        }

        public Manufacturer Create(Manufacturer manufacturer)
        {
            DbContext.Manufacturers.Add(manufacturer);
            DbContext.SaveChanges();
            return manufacturer;
        }


        public void UpdateManufacturer(Manufacturer manufacturer)
        {

            manufacturer.UpdateDate = DateTime.Now;
            DbContext.Manufacturers.Update(manufacturer);
            DbContext.SaveChanges();
        }

        public void DeleteManufacturer(long Id)
        {
            var manufacturer = DbContext.Manufacturers.Find(Id);
            if(manufacturer != null)
            {
                manufacturer.IsDelete = true;
                manufacturer.IsDisplay = false;
                DbContext.Manufacturers.Update(manufacturer);
                DbContext.SaveChanges();
            }    
                
        }
        public async Task<IEnumerable<Manufacturer>> GetAllManufacturer()
        {
            var GetAllManufacturer = await DbContext.Manufacturers
                .Where(c => c.IsDelete.Equals(false) && c.IsDisplay.Equals(true)).ToListAsync();
            return GetAllManufacturer;
        }
    }
}
