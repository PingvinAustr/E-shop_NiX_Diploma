using System.Net;
using Catalog.Host.Configurations;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Responses;
using Catalog.Host.Services.Interfaces;
using Infrastructure;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Catalog.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogBffController : ControllerBase
{
    private readonly ILogger<CatalogBffController> _logger;
    private readonly ICatalogService _catalogService;
    private readonly IOptions<CatalogConfig> _config;

    public CatalogBffController(
        ILogger<CatalogBffController> logger,
        ICatalogService catalogService,
        IOptions<CatalogConfig> config)
    {
        _logger = logger;
        _catalogService = catalogService;
        _config = config;
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(PaginatedItemsResponse<CarDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Items(PaginatedItemsRequest<CatalogTypeFilter> request)
    {
        _logger.LogInformation($"[LOG][{DateTime.Now}] CatalogBffController.Items - getting all items. PageSize:{request.PageSize}, PageIndex:{request.PageIndex}");
        var result = await _catalogService.GetCatalogItemsAsync(request.PageSize, request.PageIndex, request.Filters);
        _logger.LogInformation($"[LOG][{DateTime.Now}] CatalogBffController.Items - got {result.Count} items");
        return Ok(result);
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(PaginatedItemsResponse<ManufacturerDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetManufacturers()
    {
        _logger.LogInformation($"User Id {User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value}");
        _logger.LogInformation($"[LOG][{DateTime.Now}] CatalogBffContoller.GetManufacturers - getting all manufacturers");
        var items = await _catalogService.GetManufacturers();
        if (items == null)
        {
            _logger.LogError($"[LOG][{DateTime.Now}] CatalogBffContoller.GetManufacturers - not found");
            return NotFound();
        }

        _logger.LogInformation($"[LOG][{DateTime.Now}] CatalogBffContoller.GetManufacturers - got {items.Count} manufacturers");
        return Ok(items);
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(PaginatedItemsResponse<TypeDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetTypes()
    {
        _logger.LogInformation($"[LOG][{DateTime.Now}] CatalogBffContoller.GetTypes - getting all types");
        var items = await _catalogService.GetTypes();
        if (items == null)
        {
            _logger.LogError($"[LOG][{DateTime.Now}] CatalogBffContoller.GetTypes - not found");
            return NotFound();
        }

        _logger.LogInformation($"[LOG][{DateTime.Now}] CatalogBffContoller.GetTypes - got {items.Count} types");
        return Ok(items);
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(CarDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetById(int id)
    {
        _logger.LogInformation($"[LOG][{DateTime.Now}] CatalogBffContoller.GetById - getting car by ID:{id}");
        if (id < 0)
        {
            _logger.LogError($"[LOG][{DateTime.Now}] CatalogBffContoller.GetById - ID validation error - ID cannot be <0");
            return BadRequest("ID cannot be <0");
        }

        var item = await _catalogService.GetById(id);
        if (item == null)
        {
            _logger.LogError($"[LOG][{DateTime.Now}] CatalogBffContoller.GetById - not found car with ID:{id}");
            return NotFound();
        }

        _logger.LogInformation($"[LOG][{DateTime.Now}] CatalogBffContoller.GetById - successfully returned car with ID:{id}");
        return Ok(item);
    }

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedItemsResponse<CarDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetByBrand(int brandId)
    {
        _logger.LogInformation($"[LOG][{DateTime.Now}] CatalogBffContoller.GetByBrand - getting cars by brand with ID:{brandId}");
        if (brandId < 0)
        {
            _logger.LogError($"[LOG][{DateTime.Now}] CatalogBffContoller.GetByBrand - ID validation error - ID cannot be <0");
            return BadRequest("Id cannot be <0");
        }

        var items = await _catalogService.GetByBrand(brandId);
        if (items == null)
        {
            _logger.LogError($"[LOG][{DateTime.Now}] CatalogBffContoller.GetByBrand - not found cars with brand ID:{brandId}");
            return NotFound();
        }

        _logger.LogInformation($"[LOG][{DateTime.Now}] CatalogBffContoller.GetByBrand - successfully returned {items.Count} cars with brand ID:{brandId}");
        return Ok(items);
    }

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedItemsResponse<Car>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetByType(int typeId)
    {
        _logger.LogInformation($"[LOG][{DateTime.Now}] CatalogBffContoller.GetByType - getting cars by type with ID:{typeId}");
        if (typeId < 0)
        {
            _logger.LogError($"[LOG][{DateTime.Now}] CatalogBffContoller.GetByType - ID validation error - ID cannot be <0");
            return BadRequest("Id cannot be <0");
        }

        var items = await _catalogService.GetByType(typeId);
        if (items == null)
        {
            _logger.LogError($"[LOG][{DateTime.Now}] CatalogBffContoller.GetByType - not found cars with type ID:{typeId}");
            return NotFound();
        }

        _logger.LogInformation($"[LOG][{DateTime.Now}] CatalogBffContoller.GetByType - successfully returned {items.Count} cars with type ID:{typeId}");
        return Ok(items);
    }
}