using System.Net;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Responses;
using Catalog.Host.Services.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
public class ManufacturerController : ControllerBase
{
    private readonly ILogger<ManufacturerController> _logger;
    private readonly IManufacturerService _brandService;

    public ManufacturerController(ILogger<ManufacturerController> logger, IManufacturerService brandService)
    {
        _logger = logger;
        _brandService = brandService;
    }

    [HttpPost]
    public async Task<IActionResult> Add(string brand, string country)
    {
        var result = await _brandService.Add(brand, country);
        return Ok(new AddCarResponse<int?>() { Id = result });
    }

    [HttpDelete]
    public IActionResult Delete(int itemId)
    {
        var result = _brandService.Delete(itemId);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put(ManufacturerDto item, int itemToUpdate)
    {
        var result = await _brandService.Put(item, itemToUpdate);
        return Ok();
    }
}