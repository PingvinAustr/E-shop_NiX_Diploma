using IdentityModel;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using MVC.Services;
using MVC.Services.Interfaces;
using MVC.ViewModels;

namespace MVC.Controllers;
[ApiController]
[Route("[controller]")]
[Authorize]
public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly IIdentityParser<ApplicationUser> _identityParser;

    public AccountController(
        ILogger<AccountController> logger,
        IIdentityParser<ApplicationUser> identityParser)
    {
        _logger = logger;
        _identityParser = identityParser;
    }

    [Route("SignIn")]
    public IActionResult SignIn()
    {
        var user = _identityParser.Parse(User);

        _logger.LogInformation($"User {user.Name} authenticated");
        
        // "Catalog" because UrlHelper doesn't support nameof() for controllers
        // https://github.com/aspnet/Mvc/issues/5853
        return RedirectToAction(nameof(CatalogController.Index), "Catalog");
    }


    [Route("Signout")]
    public async Task<IActionResult> Signout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
        // "Catalog" because UrlHelper doesn't support nameof() for controllers
        // https://github.com/aspnet/Mvc/issues/5853
        var homeUrl = Url.Action(nameof(CatalogController.Index), "Catalog");
        return new SignOutResult(OpenIdConnectDefaults.AuthenticationScheme,
            new AuthenticationProperties { RedirectUri = homeUrl });
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("AccountData")]
    public IActionResult AccountData()
    {
        Dictionary<string, string> profile = new Dictionary<string, string>();
        // Extract claims from user identity
        string givenName = User.FindFirst(JwtClaimTypes.GivenName)?.Value;
        string familyName = User.FindFirst(JwtClaimTypes.FamilyName)?.Value;

        // Add claims to dictionary
        profile.Add("given_name", givenName);
        profile.Add("family_name", familyName);

        return Ok(profile);
    }
}
