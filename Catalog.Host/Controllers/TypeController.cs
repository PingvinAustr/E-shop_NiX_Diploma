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
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _typeService.Add(request.TypeName, request.TypeDescription);
        return Ok(new AddCarResponse<int?>() { Id = result });
    }

    [HttpDelete]
    public IActionResult Delete(int itemId)
    {
        if (itemId < 0)
        {
            return BadRequest("Id cannot be <0");
        }

        var result = _typeService.Delete(itemId);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] TypeDto item, int itemToUpdate)
    {
        var validationResult = await new TypeDtoValidator().ValidateAsync(item);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var result = await _typeService.Put(item, itemToUpdate);
        return Ok();
    }
}