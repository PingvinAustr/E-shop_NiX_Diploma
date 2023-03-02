using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Catalog.Host.Models.Dtos;
using AutoMapper;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Services
{
    public class CarService : BaseDataService<AppDbContext>, ICarService
    {
        private readonly ICarRepository _catalogItemRepository;
        private readonly IMapper _mapper;

        public CarService(
            Infrastructure.Services.Interfaces.IDbContextWrapper<AppDbContext> dbContextWrapper,
            ILogger<BaseDataService<AppDbContext>> logger,
            ICarRepository catalogItemRepository,
            IMapper mapper)
            : base(dbContextWrapper, logger)
        {
            _catalogItemRepository = catalogItemRepository;
            _mapper = mapper;
        }

        public Task<int?> Add(string name, string promo, int price, bool isAvailable, int carTypeId, int carManufaturerid, string imageFileName)
        {
            return ExecuteSafeAsync(() => _catalogItemRepository.Add(name, promo, price, isAvailable, carTypeId, carManufaturerid, imageFileName));
        }

        public bool Delete(int itemId)
        {
            return _catalogItemRepository.Delete(itemId);
        }

        public Task<int?> Put(CarDto item, int itemToUpdate)
        {
            var catalogItem = _mapper.Map<Car>(item);
            return ExecuteSafeAsync(() => _catalogItemRepository.Put(catalogItem, itemToUpdate));
        }
    }
}
