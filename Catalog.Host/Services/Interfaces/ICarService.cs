using Catalog.Host.Models.Dtos;
namespace Catalog.Host.Services.Interfaces
{
    public interface ICarService
    {
        Task<int?> Add(string name, string promo, int price, bool isAvailable, int carTypeId, int carManufaturerid, string imageFileName);
        bool Delete(int id);
        Task<int?> Put(CarDto item, int itemToUpdate);
    }
}
