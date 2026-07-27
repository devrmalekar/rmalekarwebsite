using Microsoft.AspNetCore.Mvc;
using Portfolio.Api.Interfaces;

namespace Portfolio.Api.Controllers;

[ApiController]
[Route("api/theme")]
public class ThemeController : ControllerBase
{
    private readonly IModeCacheService _cache;

    public ThemeController(IModeCacheService cache) => _cache = cache;

    [HttpPost("explore")]
    public async Task<IActionResult> ExploreTheme([FromQuery] string theme)
    {
        var valid = new[] { "classic", "developer", "space" };

        if (!valid.Contains(theme))
        {
            return BadRequest("Invalid theme");
        }

        var userKey = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        await _cache.SetTheme(userKey, theme);

        return Ok(new { explore = theme });
    }
}
