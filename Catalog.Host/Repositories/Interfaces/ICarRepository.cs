using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICarRepository
    {
        Task<PaginatedItems<Car>> GetByPageAsync(int pageIndex, int pageSize);
        Task<int?> Add(string name, string promo, int price, bool isAvailable, int carTypeId, int carManufaturerid, string imageFileName);
        Task<PaginatedItems<Manufacturer>> GetBrands();
    }
}
