using System.Net;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Responses;
using Catalog.Host.Models.Validators;
using Catalog.Host.Services.Interfaces;
using Infrastructure;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowClientPolicy)]
[Scope("catalog.catalogitem")]
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
            _logger.LogError($"[LOG][{DateTime.Now}] CarController.Add - Failed to add car {request.CarName} - bad request");
            return BadRequest(ModelState);
        }

        _logger.LogInformation($"[LOG][{DateTime.Now}] CarController.Add - Adding car {request.CarName}");
        try
        {
            var result = await _catalogItemService.Add(request.CarName, request.CarPromo, request.Price, request.IsAvailable, request.ManufacturerId, request.CarTypeId, request.ImageFileName);
            _logger.LogInformation($"[LOG][{DateTime.Now}] CarController.Add - Added car {request.CarName}");
            return Ok(new AddCarResponse<int?>() { Id = result });
        }
        catch (Exception ex)
        {
            _logger.LogError($"[LOG][{DateTime.Now}] CarController.Add - Failed to add car: {request.CarName} due to exception - {ex.Message}. Stack trace - {ex.StackTrace}");
            return BadRequest(ex);
        }
    }

    [HttpDelete]
    public IActionResult Delete(int itemId)
    {
        if (itemId < 0)
        {
            _logger.LogError($"[LOG][{DateTime.Now}] CarController.Delete - Failed to delete car with ID:{itemId} - bad request");
            return BadRequest("Id cannot be <0");
        }

        _logger.LogInformation($"[LOG][{DateTime.Now}] CarController.Delete - Deleting car with ID:{itemId}");
        try
        {
            var result = _catalogItemService.Delete(itemId);
            _logger.LogInformation($"[LOG][{DateTime.Now}] CarController.Delete - Deleted car with ID:{itemId}");
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError($"[LOG][{DateTime.Now}] CarController.Delete - Failed to delete car with ID: {itemId} due to exception - {ex.Message}. Stack trace - {ex.StackTrace}");
            return BadRequest(ex);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] CarDto item, int itemToUpdate)
    {
        var validationResult = await new CarDtoValidator().ValidateAsync(item);
        if (!validationResult.IsValid)
        {
            _logger.LogError($"[LOG][{DateTime.Now}] CarController.Put - Failed to edit(PUT) car with ID:{item.CarId} - model validation error - {validationResult.Errors}");
            return BadRequest(validationResult.Errors);
        }

        _logger.LogInformation($"[LOG][{DateTime.Now}] CarController.Put - Trying to update car with ID:{item.CarId}");
        try
        {
            var result = await _catalogItemService.Put(item, itemToUpdate);
            _logger.LogInformation($"[LOG][{DateTime.Now}] CarController.Put - Updated car with ID:{item.CarId}");
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError($"[LOG][{DateTime.Now}] CarController.Put - Failed to delete car with ID: {item.CarId} due to exception - {ex.Message}. Stack trace - {ex.StackTrace}");
            return BadRequest(ex);
        }
    }
}