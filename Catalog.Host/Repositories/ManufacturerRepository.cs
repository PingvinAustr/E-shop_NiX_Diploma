using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Catalog.Host.Repositories;

public class ManufacturerRepository : IManufacturerRepository
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<ManufacturerRepository> _logger;

    public ManufacturerRepository(
        IDbContextWrapper<AppDbContext> dbContextWrapper,
        ILogger<ManufacturerRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<int?> Add(string brand, string country)
    {
        var item = await _dbContext.AddAsync(new Manufacturer { ManufacturerName = brand, ManufacturerCountry = country });

        await _dbContext.SaveChangesAsync();

        return item.Entity.ManufacturerId;
    }

    public bool Delete(int id)
    {
        try
        {
            foreach (var brand in _dbContext.Manufacturers.ToList())
            {
                if (brand.ManufacturerId == id)
                {
                    _dbContext.Manufacturers.Remove(brand);
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

    public async Task<int?> Put(Manufacturer item, int id)
    {
        _dbContext.Manufacturers.Where(x => x.ManufacturerId == id).ToList().ForEach(x =>
        {
            x.ManufacturerName = item.ManufacturerName;
            x.ManufacturerCountry = item.ManufacturerCountry;
        });
        await _dbContext.SaveChangesAsync();
        return id;
    }
}