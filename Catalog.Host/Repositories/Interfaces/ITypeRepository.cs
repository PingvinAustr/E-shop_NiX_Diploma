using Catalog.Host.Models.Dtos;
namespace Catalog.Host.Services.Interfaces;

public interface ITypeRepository
{
    Task<int?> Add(string type, string description);
    bool Delete(int id);
    Task<int?> Put(Data.Entities.Type item, int itemToUpdate);
}