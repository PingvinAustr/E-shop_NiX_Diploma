using System.Net;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Responses;
using Catalog.Host.Services.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogBffController : ControllerBase
{
    private readonly ILogger<CatalogBffController> _logger;
    private readonly ICatalogService _catalogService;

    public CatalogBffController(
        ILogger<CatalogBffController> logger,
        ICatalogService catalogService)
    {
        _logger = logger;
        _catalogService = catalogService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(PaginatedItemsResponse<CarDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Items(PaginatedItemsRequest request)
    {
        var result = await _catalogService.GetCatalogItemsAsync(request.PageSize, request.PageIndex);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedItemsResponse<ManufacturerDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetManufacturers()
    {
        var items = await _catalogService.GetBrands();
        if (items == null)
        {
            return NotFound();
        }

        return Ok(items);
    }

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedItemsResponse<TypeDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetTypes()
    {
        var items = await _catalogService.GetTypes();
        if (items == null)
        {
            return NotFound();
        }

        return Ok(items);
    }

    [HttpGet]
    [ProducesResponseType(typeof(CarDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _catalogService.GetById(id);
        if (item == null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedItemsResponse<CarDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetByBrand(int brandId)
    {
        var items = await _catalogService.GetByBrand(brandId);
        if (items == null)
        {
            return NotFound();
        }

        return Ok(items);
    }

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedItemsResponse<Car>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetByType(int typeId)
    {
        var items = await _catalogService.GetByType(typeId);
        if (items == null)
        {
            return NotFound();
        }

        return Ok(items);
    }
}