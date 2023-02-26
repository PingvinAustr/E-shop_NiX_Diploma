using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Data.Entities;

using AutoMapper;

namespace Catalog.Host.Services;

public class TypeService : BaseDataService<AppDbContext>, ITypeService
{
    private readonly ITypeRepository _catalogTypeRepository;
    private readonly IMapper _mapper;

    public TypeService(
        IDbContextWrapper<AppDbContext> dbContextWrapper,
        ILogger<BaseDataService<AppDbContext>> logger,
        ITypeRepository catalogTypeRepository,
        IMapper mapper)
        : base(dbContextWrapper, logger)
    {
        _catalogTypeRepository = catalogTypeRepository;
        _mapper = mapper;
    }

    public Task<int?> Add(string brand, string description)
    {
        return ExecuteSafeAsync(() => _catalogTypeRepository.Add(brand, description));
    }

    public bool Delete(int itemId)
    {
        return _catalogTypeRepository.Delete(itemId);
    }

    public Task<int?> Put(TypeDto item, int itemToUpdate)
    {
        var catalogItem = _mapper.Map<Data.Entities.Type>(item);
        return ExecuteSafeAsync(() => _catalogTypeRepository.Put(catalogItem, itemToUpdate));
    }
}