using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<CarRepository> _logger;

        public CarRepository(
        IDbContextWrapper<AppDbContext> dbContextWrapper,
        ILogger<CarRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        /*
        public async Task<PaginatedItems<Car>> GetByPageAsync(int pageIndex, int pageSize)
        {
            var totalItems = await _dbContext.Cars
                .LongCountAsync();

            var itemsOnPage = await _dbContext.Cars
                .Include(i => i.Manufacturer)
                .Include(i => i.CarType)
                .OrderBy(c => c.CarName)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedItems<Car>() { TotalCount = totalItems, Data = itemsOnPage };
        }
        */

        public async Task<PaginatedItems<Car>> GetByPageAsync(int pageIndex, int pageSize, int? brandFilter, int? typeFilter)
        {
            IQueryable<Car> query = _dbContext.Cars;

            if (brandFilter.HasValue)
            {
                query = query.Where(w => w.ManufacturerId == brandFilter.Value);
            }

            if (typeFilter.HasValue)
            {
                query = query.Where(w => w.TypeId == typeFilter.Value);
            }

            var totalItems = await query.LongCountAsync();

            var itemsOnPage = await query.OrderBy(c => c.Manufacturer.ManufacturerName)
               .Include(i => i.Manufacturer)
               .Include(i => i.CarType)
               .Skip(pageSize * pageIndex)
               .Take(pageSize)
               .ToListAsync();

            return new PaginatedItems<Car>() { TotalCount = totalItems, Data = itemsOnPage };
        }

        public async Task<int?> Add(string name, string promo, int price, bool isAvailable, int carTypeId, int carManufaturerid, string imageFileName)
        {
            var item = await _dbContext.AddAsync(new Car
            {
                ManufacturerId = carManufaturerid,
                TypeId = carTypeId,
                CarPromo = promo,
                CarName = name,
                ImageFileName = imageFileName,
                Price = price
            });

            await _dbContext.SaveChangesAsync();

            return item.Entity.CarId;
        }

        public async Task<PaginatedItems<Manufacturer>> GetBrands()
        {
            var items = await _dbContext.Manufacturers.ToListAsync();
            return new PaginatedItems<Manufacturer>() { Data = items, TotalCount = items.Count };
        }

        public async Task<PaginatedItems<Data.Entities.Type>> GetTypes()
        {
            var items = await _dbContext.Types.ToListAsync();
            return new PaginatedItems<Data.Entities.Type>() { Data = items, TotalCount = items.Count };
        }

        public async Task<PaginatedItems<Car>> GetById(int id)
        {
            var item = await _dbContext.Cars.Where(x => x.CarId == id).Include(x => x.Manufacturer).Include(x => x.CarType).ToListAsync();
            return new PaginatedItems<Car>() { TotalCount = 1, Data = item };
        }

        public async Task<PaginatedItems<Car>> GetByBrand(int brand)
        {
            var items = await _dbContext.Cars.Where(x => x.Manufacturer.ManufacturerId == brand).Include(x => x.Manufacturer).Include(x => x.CarType).ToListAsync();
            return new PaginatedItems<Car>() { TotalCount = items.Count, Data = items };
        }

        public async Task<PaginatedItems<Car>> GetByType(int typeId)
        {
            var items = await _dbContext.Cars.Where(x => x.CarType.TypeId == typeId).Include(x => x.CarType).Include(x => x.Manufacturer).ToListAsync();
            return new PaginatedItems<Car>() { Data = items, TotalCount = items.Count };
        }

        public bool Delete(int id)
        {
            try
            {
                foreach (var item in _dbContext.Cars.ToList())
                {
                    if (item.CarId == id)
                    {
                        _dbContext.Cars.Remove(item);
                    }
                }

                _dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<int?> Put(Car item, int id)
        {
            _dbContext.Cars.Where(x => x.CarId == id).ToList().ForEach(x =>
            {
                x.CarName = item.CarName;
                x.CarPromo = item.CarPromo;
                x.Price = item.Price;
                x.CarType = item.CarType;
                x.Manufacturer = item.Manufacturer;
                x.IsAvailable = item.IsAvailable;
                x.ImageFileName = item.ImageFileName;
            });
            await _dbContext.SaveChangesAsync();
            return id;
        }
    }
}
