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
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _brandService.Add(request.ManufacturerName, request.ManufacturerCountry);
        return Ok(new AddCarResponse<int?>() { Id = result });
    }

    [HttpDelete]
    public IActionResult Delete(int itemId)
    {
        if (itemId < 0)
        {
            return BadRequest("Id cannot be <0");
        }

        var result = _brandService.Delete(itemId);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] ManufacturerDto item, int itemToUpdate)
    {
        var validationResult = await new ManufacturerDtoValidator().ValidateAsync(item);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var result = await _brandService.Put(item, itemToUpdate);
        return Ok();
    }
}