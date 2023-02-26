using System.Net;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Responses;
using Catalog.Host.Models.Validators;
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
    [ProducesResponseType(typeof(AddCarResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add([FromBody] CreateManufacturerRequest request)
    {
        _logger.LogInformation($"[LOG][{DateTime.Now}] ManufacturerController.Add - Trying to add manufacturer with ID:{request.ManufacturerId}");
        if (!ModelState.IsValid)
        {
            _logger.LogError($"[LOG][{DateTime.Now}] ManufacturerController.Add - Failed to add manufacturer with ID:{request.ManufacturerId} due to model validation errors");
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _brandService.Add(request.ManufacturerName, request.ManufacturerCountry);
            _logger.LogInformation($"[LOG][{DateTime.Now}] ManufacturerController.Add -  Successfully added manufaturer with ID:{request.ManufacturerId}");
            return Ok(new AddCarResponse<int?>() { Id = result });
        }
        catch (Exception ex)
        {
            _logger.LogError($"[LOG][{DateTime.Now}] ManufacturerController.Add - Failed to add manufacturer with ID: {request.ManufacturerId} due to exception - {ex.Message}. Stack trace - {ex.StackTrace}");
            return BadRequest(ex);
        }
    }

    [HttpDelete]
    public IActionResult Delete(int itemId)
    {
        _logger.LogInformation($"[LOG][{DateTime.Now}] ManufacturerContoller.Delete - Trying to delete manufacturer with ID:{itemId}");
        if (itemId < 0)
        {
            _logger.LogError($"[LOG][{DateTime.Now}] ManufacturerController.Delete - Failed to delete manufacturer with ID:{itemId} - ID cannot be <0");
            return BadRequest("Id cannot be <0");
        }

        try
        {
            var result = _brandService.Delete(itemId);
            _logger.LogInformation($"[LOG][{DateTime.Now}] ManufacturerContoller.Delete - successfully deleted manufacturer with ID:{itemId}");
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError($"[LOG][{DateTime.Now}] ManufacturerController.Delete - Failed to delete manufacturer with ID: {itemId} due to exception - {ex.Message}. Stack trace - {ex.StackTrace}");
            return BadRequest(ex);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] ManufacturerDto item, int itemToUpdate)
    {
        _logger.LogInformation($"[LOG][{DateTime.Now}] ManufacturerContoller.Put - Trying to update manufacturer with ID:{itemToUpdate}");
        var validationResult = await new ManufacturerDtoValidator().ValidateAsync(item);
        if (!validationResult.IsValid)
        {
            _logger.LogError($"[LOG][{DateTime.Now}] ManufacturerController.Put - Failed to update manufacturer with ID:{itemToUpdate} due to validation errors - {validationResult.Errors}");
            return BadRequest(validationResult.Errors);
        }

        try
        {
            var result = await _brandService.Put(item, itemToUpdate);
            _logger.LogInformation($"[LOG][{DateTime.Now}] ManufacturerContoller.Put - successfully updated manufacturer with ID:{itemToUpdate}");
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError($"[LOG][{DateTime.Now}] ManufacturerController.Put - Failed to update manufacturer with ID: {item.ManufacturerId} due to exception - {ex.Message}. Stack trace - {ex.StackTrace}");
            return BadRequest(ex);
        }
    }
}