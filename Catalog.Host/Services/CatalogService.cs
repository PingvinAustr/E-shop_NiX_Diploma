using AutoMapper;
using Catalog.Host.Configurations;
using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Responses;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class CatalogService : BaseDataService<AppDbContext>, ICatalogService
    {
        private readonly ICarRepository _catalogItemRepository;
        private readonly IMapper _mapper;

        public CatalogService(
            IDbContextWrapper<AppDbContext> dbContextWrapper,
            ILogger<BaseDataService<AppDbContext>> logger,
            ICarRepository catalogItemRepository,
            IMapper mapper)
            : base(dbContextWrapper, logger)
        {
            _catalogItemRepository = catalogItemRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedItemsResponse<CarDto>> GetCatalogItemsAsync(int pageSize, int pageIndex)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _catalogItemRepository.GetByPageAsync(pageIndex, pageSize);
                return new PaginatedItemsResponse<CarDto>()
                {
                    Count = result.TotalCount,
                    Data = result.Data.Select(s => _mapper.Map<CarDto>(s)).ToList(),
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };
            });
        }

        public async Task<PaginatedItemsResponse<ManufacturerDto>> GetBrands()
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _catalogItemRepository.GetBrands();
                return new PaginatedItemsResponse<ManufacturerDto>()
                {
                    Count = result.TotalCount,
                    Data = result.Data.Select(s => _mapper.Map<ManufacturerDto>(s)).ToList(),
                };
            });
        }
    }
}
