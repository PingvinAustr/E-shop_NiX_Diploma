using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class CarService : BaseDataService<AppDbContext>, ICarService
    {
        private readonly ICarRepository _catalogItemRepository;

        public CarService(
            IDbContextWrapper<AppDbContext> dbContextWrapper,
            ILogger<BaseDataService<AppDbContext>> logger,
            ICarRepository catalogItemRepository)
            : base(dbContextWrapper, logger)
        {
            _catalogItemRepository = catalogItemRepository;
        }

        public Task<int?> Add(string name, string promo, int price, bool isAvailable, int carTypeId, int carManufaturerid, string imageFileName)
        {
            return ExecuteSafeAsync(() => _catalogItemRepository.Add(name, promo, price, isAvailable, carTypeId, carManufaturerid, imageFileName));
        }
    }
}
