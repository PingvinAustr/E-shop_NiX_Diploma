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
public class TypeController : ControllerBase
{
    private readonly ILogger<TypeController> _logger;
    private readonly ITypeService _typeService;

    public TypeController(ILogger<TypeController> logger, ITypeService typeService)
    {
        _logger = logger;
        _typeService = typeService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddCarResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add([FromBody] CreateTypeRequest request)
    {
        _logger.LogInformation($"[LOG][{DateTime.Now}]TypeController.Add - Trying to add type {request.TypeName}");
        if (!ModelState.IsValid)
        {
            _logger.LogInformation($"[LOG][{DateTime.Now}]TypeController.Add - failed to add type {request.TypeName} due to validation erros");
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _typeService.Add(request.TypeName, request.TypeDescription);
            _logger.LogInformation($"[LOG][{DateTime.Now}]TypeController.Add - successfully added type {request.TypeName}");
            return Ok(new AddCarResponse<int?>() { Id = result });
        }
        catch (Exception ex)
        {
            _logger.LogError($"[LOG][{DateTime.Now}]TypeController.Add - failed to add type {request.TypeName}, exception occured - {ex.Message}. Stack trace - {ex.StackTrace}");
            return BadRequest(ModelState);
        }
    }

    [HttpDelete]
    public IActionResult Delete(int itemId)
    {
        _logger.LogInformation($"[LOG][{DateTime.Now}]TypeController.Delete - Trying to delete type with ID:{itemId}");
        if (itemId < 0)
        {
            _logger.LogInformation($"[LOG][{DateTime.Now}]TypeController.Delete - Failed to delete type with ID:{itemId} - ID cannot be <0");
            return BadRequest("Id cannot be <0");
        }

        try
        {
            var result = _typeService.Delete(itemId);
            _logger.LogInformation($"[LOG][{DateTime.Now}]TypeController.Delete - successfully deleted type with ID: {itemId}");
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError($"[LOG][{DateTime.Now}]TypeController.Delete - failed to delete type with ID:{itemId}, exception occured - {ex.Message}. Stack trace - {ex.StackTrace}");
            return BadRequest(ModelState);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] TypeDto item, int itemToUpdate)
    {
        _logger.LogInformation($"[LOG][{DateTime.Now}]TypeController.Delete - Trying to update type with ID:{item.TypeId}");
        var validationResult = await new TypeDtoValidator().ValidateAsync(item);
        if (!validationResult.IsValid)
        {
            _logger.LogInformation($"[LOG][{DateTime.Now}]TypeController.Put - Failed to update type with ID:{item.TypeId} due to validation errors - {validationResult.Errors}");
            return BadRequest(validationResult.Errors);
        }

        try
        {
            var result = await _typeService.Put(item, itemToUpdate);
            _logger.LogInformation($"[LOG][{DateTime.Now}]TypeController.Put - successfully updated type with ID: {item.TypeId}");
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError($"[LOG][{DateTime.Now}]TypeController.Put - failed to update type with ID:{item.TypeId}, exception occured - {ex.Message}. Stack trace - {ex.StackTrace}");
            return BadRequest(ModelState);
        }
    }
}