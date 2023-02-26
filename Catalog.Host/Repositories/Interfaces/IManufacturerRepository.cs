using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
namespace Catalog.Host.Repositories.Interfaces;

public interface IManufacturerRepository
{
    Task<int?> Put(Manufacturer item, int id);
    bool Delete(int id);
    Task<int?> Add(string brand, string country);
}