using System.Net.Http.Json;
using Portfolio.ClassicMode.IServices;
using Portfolio.ClassicMode.Services;
using Portfolio.Shared.Dtos;

namespace Portfolio.ClassicMode.Services;

public class PortfolioService
{
    private readonly HttpClient _http;
    private readonly HybridCacheService _cache;

    public PortfolioService(HttpClient http, HybridCacheService cache)
    {
        _http = http;
        _cache = cache;
    }

    private async Task<T> GetDataAsync<T>(string key, string apiUrl)
    {
        try
        {
            var (source, dto) = await _cache.GetAsync(
                key,
                () => _http.GetFromJsonAsync<T>(apiUrl),
                TimeSpan.FromMinutes(2)
            );
            // Console.WriteLine($"{key.ToUpperInvariant()} From : {source}");
            return dto!;
        }
        catch (Exception ex)
        {
            await _cache.RemoveAsync(key);
        }
        return default!;
    }

    public async Task<PortfolioDto> GetPortfolioAsync()
    {
        return await GetDataAsync<PortfolioDto>("portfolio", "api/portfolio");
    }

    public async Task<HeroDto> GetHeroDetailAsync()
    {
        return await GetDataAsync<HeroDto>("hero", "api/portfolio/hero");
    }

    public async Task<PersonalDetailDto> GetPersonalDetailAsync()
    {
        return await GetDataAsync<PersonalDetailDto>(
            "personalDetail",
            "api/portfolio/personaldetail"
        );
    }

    public async Task<List<CertificationDto>> GetCertificationsAsync()
    {
        return await GetDataAsync<List<CertificationDto>>(
            "certifications",
            "api/portfolio/certifications"
        );
    }

    public async Task<List<ExpereinceGroupDto>?> GetExperienceAsync()
    {
        return await GetDataAsync<List<ExpereinceGroupDto>>(
            "experience",
            "api/portfolio/experience"
        );
    }

    public async Task<List<SkillAccordionDto>> GetSkillsAsync()
    {
        return await GetDataAsync<List<SkillAccordionDto>>("skills", "api/portfolio/skills");
    }

    public async Task<List<QualificationDto>> GetQualificationsAsync()
    {
        return await GetDataAsync<List<QualificationDto>>(
            "qualifications",
            "api/portfolio/qualifications"
        );
    }

    public async Task<List<ProjectDto>> GetProjectsAsync()
    {
        return await GetDataAsync<List<ProjectDto>>("projects", "api/portfolio/projects");
    }
}
