using Catalog.Host.Models.Dtos;
namespace Catalog.Host.Services.Interfaces;

public interface ITypeService
{
    Task<int?> Add(string type, string description);
    bool Delete(int id);
    Task<int?> Put(TypeDto item, int itemToUpdate);
}