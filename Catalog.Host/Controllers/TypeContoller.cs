using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
public class TypeContoller : ControllerBase
{
    private readonly ILogger<TypeContoller> _logger;

    public TypeContoller(ILogger<TypeContoller> logger)
    {
        _logger = logger;
    }
}