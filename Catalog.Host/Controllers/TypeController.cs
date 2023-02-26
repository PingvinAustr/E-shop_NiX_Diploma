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
    public async Task<IActionResult> Add(string type, string description)
    {
        var result = await _typeService.Add(type, description);
        return Ok(new AddCarResponse<int?>() { Id = result });
    }

    [HttpDelete]
    public IActionResult Delete(int itemId)
    {
        var result = _typeService.Delete(itemId);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put(TypeDto item, int itemToUpdate)
    {
        var result = await _typeService.Put(item, itemToUpdate);
        return Ok();
    }
}