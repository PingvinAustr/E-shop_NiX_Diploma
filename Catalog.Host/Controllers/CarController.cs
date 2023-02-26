using System.Net;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Responses;
using Catalog.Host.Services.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Validators;

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
    public async Task<IActionResult> Add([FromBody] CreateCarRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _catalogItemService.Add(request.CarName, request.CarPromo, request.Price, request.IsAvailable, request.ManufacturerId, request.CarTypeId, request.ImageFileName);
        return Ok(new AddCarResponse<int?>() { Id = result });
    }

    [HttpDelete]
    public IActionResult Delete(int itemId)
    {
        if (itemId < 0)
        {
            return BadRequest("Id cannot be <0");
        }

        var result = _catalogItemService.Delete(itemId);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] CarDto item, int itemToUpdate)
    {
        var validationResult = await new CarDtoValidator().ValidateAsync(item);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var result = await _catalogItemService.Put(item, itemToUpdate);
        return Ok();
    }
}