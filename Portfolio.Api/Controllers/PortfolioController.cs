using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Portfolio.Api.Interfaces;

namespace Portfolio.Api.Controllers;

[EnableRateLimiting("PortfolioLimiter")]
[ApiController]
[Route("api/[controller]")]
public class PortfolioController : ControllerBase
{
    private readonly IPortfolioService _service;

    public PortfolioController(IPortfolioService service)
    {
        _service = service;
    }

    // GET: api/portfolio
    [HttpGet]
    public async Task<IActionResult> GetPortfolio() => Ok(await _service.GetPortfolioAsync());

    //GET: api/portfolio/hero
    [HttpGet("hero")]
    public async Task<IActionResult> GetHero() => Ok(await _service.GetHeroDetailAsync());

    // GET: api/portfolio/personaldetail
    [HttpGet("personaldetail")]
    public async Task<IActionResult> GetPersonalDetail() =>
        Ok(await _service.GetPersonalDetailAsync());

    // GET: api/portfolio/certifications
    [HttpGet("certifications")]
    public async Task<IActionResult> GetCertifications() =>
        Ok(await _service.GetCertificationsAsync());

    // GET: api/portfolio/experience
    [HttpGet("experience")]
    public async Task<IActionResult> GetExperience() => Ok(await _service.GetExperienceAsync());

    // GET: api/portfolio/skills
    [HttpGet("skills")]
    public async Task<IActionResult> GetSkills() => Ok(await _service.GetSkillsAsync());

    // GET: api/portfolio/qualifications
    [HttpGet("qualifications")]
    public async Task<IActionResult> GetQualifications() =>
        Ok(await _service.GetQualificationsAsync());

    // GET: api/portfolio/projects
    [HttpGet("projects")]
    public async Task<IActionResult> GetProjects() => Ok(await _service.GetProjectsAsync());
}
