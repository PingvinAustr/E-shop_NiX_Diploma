using System.Net;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Responses;
using Catalog.Host.Services.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
public class CarController : ControllerBase
{
    private readonly ILogger<CarController> _logger;
    private readonly ICarService _catalogItemService;

    public CarController(
        ILogger<CarController> logger,
        ICarService catalogItemService)
    {
        _logger = logger;
        _catalogItemService = catalogItemService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddCarResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(CreateCarRequest request)
    {
        var result = await _catalogItemService.Add(request.CarName, request.CarPromo, request.Price, request.IsAvailable, request.ManufacturerId, request.CarTypeId, request.ImageFileName);
        return Ok(new AddCarResponse<int?>() { Id = result });
    }
}