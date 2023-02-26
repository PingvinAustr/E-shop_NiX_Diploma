using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Responses;

namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<PaginatedItemsResponse<CarDto>> GetCatalogItemsAsync(int pageSize, int pageIndex);
        Task<PaginatedItemsResponse<ManufacturerDto>> GetBrands();
    }
}
