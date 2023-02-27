using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICarRepository
    {
        Task<PaginatedItems<Car>> GetByPageAsync(int pageIndex, int pageSize, int? brandFilter, int? typeFilter);
        Task<int?> Add(string name, string promo, int price, bool isAvailable, int carTypeId, int carManufaturerid, string imageFileName);
        Task<PaginatedItems<Manufacturer>> GetManufacturers();
        Task<PaginatedItems<Data.Entities.Type>> GetTypes();
        Task<PaginatedItems<Car>> GetById(int id);
        Task<PaginatedItems<Car>> GetByType(int typeId);
        Task<PaginatedItems<Car>> GetByBrand(int brandId);
        Task<int?> Put(Car item, int id);
        bool Delete(int id);
    }
}
