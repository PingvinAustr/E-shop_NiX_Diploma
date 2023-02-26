using Catalog.Host.Models.Dtos;
namespace Catalog.Host.Services.Interfaces;

public interface IManufacturerService
{
    Task<int?> Add(string brand, string country);
    bool Delete(int id);
    Task<int?> Put(ManufacturerDto item, int itemToUpdate);
}