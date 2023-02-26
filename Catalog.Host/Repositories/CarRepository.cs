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
    }
}
