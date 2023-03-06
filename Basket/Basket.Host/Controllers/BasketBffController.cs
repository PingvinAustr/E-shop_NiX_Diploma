using Basket.Host.Models;
using Basket.Host.Services.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Basket.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
[Scope("openid")]
[Route(ComponentDefaults.DefaultRoute)]
public class BasketBffController : ControllerBase
{
    private readonly ILogger<BasketBffController> _logger;
    private readonly IBasketService _basketService;

    public BasketBffController(
        ILogger<BasketBffController> logger,
        IBasketService basketService)
    {
        _logger = logger;
        _basketService = basketService;
    }
    
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> AddProduct(CarAddRequest data)
    {
        Console.WriteLine("[gaa" + HttpContext.Session.GetInt32("car1") + "]");
        HttpContext.Session.SetInt32("car1", 1);
        Console.WriteLine(HttpContext.Session.GetInt32("car1"));
        Console.WriteLine(HttpContext.Session.GetInt32("car1"));
        Console.WriteLine(HttpContext.Session.GetInt32("car1"));
        Console.WriteLine(HttpContext.Session.GetInt32("car1"));
        Console.WriteLine(HttpContext.Session.GetInt32("car1"));
        await _basketService.AddProduct("1", data);
        return Ok();
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(TestGetResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> TestGet()
    {
        var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
        Console.WriteLine(basketId);
        Console.WriteLine(basketId);
        Console.WriteLine(basketId);
        Console.WriteLine(basketId);
        Console.WriteLine(basketId);
        Console.WriteLine(basketId);
        Console.WriteLine("["+ User?.Identity?.Name + "]");
        var response = await _basketService.TestGet(basketId!);
        return Ok(response);
    }
}