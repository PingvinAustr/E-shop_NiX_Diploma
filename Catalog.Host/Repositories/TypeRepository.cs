using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Catalog.Host.Repositories;

public class TypeRepository : ITypeRepository
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<TypeRepository> _logger;

    public TypeRepository(
        Infrastructure.Services.Interfaces.IDbContextWrapper<AppDbContext> dbContextWrapper,
        ILogger<TypeRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<int?> Add(string type, string description)
    {
        var item = await _dbContext.AddAsync(new Data.Entities.Type { TypeName = type, TypeDescription = description });

        await _dbContext.SaveChangesAsync();

        return item.Entity.TypeId;
    }

    public bool Delete(int id)
    {
        try
        {
            foreach (var catalogType in _dbContext.Types)
            {
                if (catalogType.TypeId == id)
                {
                    _dbContext.Types.Remove(catalogType);
                }
            }

            _dbContext.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<int?> Put(Data.Entities.Type item, int id)
    {
        _dbContext.Types.Where(x => x.TypeId == id).ToList().ForEach(x =>
        {
            x.TypeName = item.TypeName;
            x.TypeDescription = item.TypeDescription;
        });
        await _dbContext.SaveChangesAsync();
        return id;
    }
}