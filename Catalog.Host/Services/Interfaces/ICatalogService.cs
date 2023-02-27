using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Responses;

namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<PaginatedItemsResponse<CarDto>?> GetCatalogItemsAsync(int pageSize, int pageIndex, Dictionary<CatalogTypeFilter, int>? filters);
        Task<PaginatedItemsResponse<ManufacturerDto>> GetManufacturers();
        Task<PaginatedItemsResponse<TypeDto>> GetTypes();
        Task<PaginatedItemsResponse<CarDto>> GetById(int id);
        Task<PaginatedItemsResponse<CarDto>> GetByBrand(int brandId);
        Task<PaginatedItemsResponse<CarDto>> GetByType(int typeId);
    }
}
