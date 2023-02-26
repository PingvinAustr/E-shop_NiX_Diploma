using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Data.Entities;

using AutoMapper;

namespace Catalog.Host.Services;

public class ManufacturerService : BaseDataService<AppDbContext>, IManufacturerService
{
    private readonly IManufacturerRepository _catalogBrandRepository;
    private readonly IMapper _mapper;

    public ManufacturerService(
        IDbContextWrapper<AppDbContext> dbContextWrapper,
        ILogger<BaseDataService<AppDbContext>> logger,
        IManufacturerRepository catalogBrandRepository,
        IMapper mapper)
        : base(dbContextWrapper, logger)
    {
        _catalogBrandRepository = catalogBrandRepository;
        _mapper = mapper;
    }

    public Task<int?> Add(string brand, string country)
    {
        return ExecuteSafeAsync(() => _catalogBrandRepository.Add(brand, country));
    }

    public bool Delete(int itemId)
    {
        return _catalogBrandRepository.Delete(itemId);
    }

    public Task<int?> Put(ManufacturerDto item, int itemToUpdate)
    {
        var catalogItem = _mapper.Map<Manufacturer>(item);
        return ExecuteSafeAsync(() => _catalogBrandRepository.Put(catalogItem, itemToUpdate));
    }
}