using MVC.Dtos;
using MVC.Models.Enums;
using MVC.Services.Interfaces;
using MVC.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace MVC.Services;

public class CatalogService : ICatalogService
{
    private readonly IOptions<AppSettings> _settings;
    private readonly IHttpClientService _httpClient;
    private readonly ILogger<CatalogService> _logger;

    public CatalogService(IHttpClientService httpClient, ILogger<CatalogService> logger, IOptions<AppSettings> settings)
    {
        _httpClient = httpClient;
        _settings = settings;
        _logger = logger;
    }

    public async Task<Catalog> GetCatalogItems(int page, int take, int? brand, int? type)
    {
        var filters = new Dictionary<CatalogTypeFilter, int>();

        if (brand.HasValue)
        {
            filters.Add(CatalogTypeFilter.Brand, brand.Value);
        }

        if (type.HasValue)
        {
            filters.Add(CatalogTypeFilter.Type, type.Value);
        }

        var result = await _httpClient.SendAsync<Catalog, PaginatedItemsRequest<CatalogTypeFilter>>($"{_settings.Value.CatalogUrl}/items",
           HttpMethod.Post,
           new PaginatedItemsRequest<CatalogTypeFilter>()
           {
               PageIndex = page,
               PageSize = take,
               Filters = filters
           });

        return result;
    }

    public async Task<IEnumerable<SelectListItem>> GetBrands()
    {
        var url = "http://www.alevelwebsite.com:5000/api/v1/CatalogBff/GetManufacturers";
        var method = HttpMethod.Get;
        List<CatalogBrand> brands = null;

        try
        {
            var response = await _httpClient.SendAsync<JObject, object>(url, method, null);

            if (response != null && response.TryGetValue("data", out JToken dataToken))
            {
                brands = JsonConvert.DeserializeObject<List<CatalogBrand>>(dataToken.ToString());
            }
        }
        catch (Exception ex)
        {
            // Handle exception
        }

        var list = new List<SelectListItem>();

        if (brands != null && brands.Any())
        {
            foreach (var brand in brands)
            {
                list.Add(new SelectListItem
                {
                    Value = brand.ManufacturerId.ToString(),
                    Text = brand.ManufacturerName
                });
            }
        }

        return list;
    }

    public async Task<IEnumerable<SelectListItem>> GetTypes()
    {
        var url = "http://www.alevelwebsite.com:5000/api/v1/CatalogBff/GetTypes";
        var method = HttpMethod.Get;
        List<CatalogType> brands = null;

        try
        {
            var response = await _httpClient.SendAsync<JObject, object>(url, method, null);

            if (response != null && response.TryGetValue("data", out JToken dataToken))
            {
                brands = JsonConvert.DeserializeObject<List<CatalogType>>(dataToken.ToString());
            }
        }
        catch (Exception ex)
        {
            // Handle exception
        }

        var list = new List<SelectListItem>();

        if (brands != null && brands.Any())
        {
            foreach (var brand in brands)
            {
                list.Add(new SelectListItem
                {
                    Value = brand.TypeId.ToString(),
                    Text = brand.TypeName
                });
            }
        }

        return list;
    }
}
