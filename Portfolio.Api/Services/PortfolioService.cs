using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Portfolio.Api.Context;
using Portfolio.Api.Interfaces;
using Portfolio.Api.Models;
using Portfolio.Shared.Dtos;

public class PortfolioService : IPortfolioService
{
    private readonly PortfolioDbContext _context;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;

    private readonly ILogger<PortfolioService> _logger;

    public PortfolioService(
        PortfolioDbContext context,
        IMapper mapper,
        IMemoryCache cache,
        ILogger<PortfolioService> logger
    )
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
        _logger = logger;
    }

    // -----------------------------
    // Master Portfolio (cached)
    // -----------------------------
    public async Task<PortfolioDto?> GetPortfolioAsync()
    {
        if (_cache.TryGetValue("Portfolio_Master", out PortfolioDto? cached))
        {
            _logger.LogInformation("Cache Hit: ");
            return cached;
        }

        _logger.LogInformation("Cache MISS: Portfolio:Projects");

        return await _cache.GetOrCreateAsync(
            "Portfolio_Master",
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2);

                return new PortfolioDto
                {
                    Personal = _mapper.Map<PersonalDetailDto>(
                        await (_context.PersonalDetails.FirstOrDefaultAsync())
                            ?? new PersonalDetail()
                    ),
                    Certifications =
                        _mapper.Map<List<CertificationDto>>(
                            await _context.Certifications.ToListAsync()
                        ) ?? new(),
                    Experience =
                        _mapper.Map<List<ExperienceDto>>(await _context.Experiences.ToListAsync())
                        ?? new(),
                    Skills =
                        _mapper.Map<List<SkillDto>>(
                            await _context.Skills.GroupBy(s => s.SkillAccordionId).ToListAsync()
                        ) ?? new(),
                    Qualifications =
                        _mapper.Map<List<QualificationDto>>(
                            await _context.Qualifications.ToListAsync()
                        ) ?? new(),
                    Projects =
                        _mapper.Map<List<ProjectDto>>(
                            await _context
                                .Projects.Include(p => p.Duties)
                                .Include(p => p.KeySkills)
                                    .ThenInclude(ks => ks.Skill)
                                .ToListAsync()
                        ) ?? new(),
                };
            }
        );
    }

    public async Task<HeroDto?> GetHeroDetailAsync()
    {
        if (_cache.TryGetValue("Hero_Intro", out HeroDto? cached))
        {
            if (cached != null)
            {
                _logger.LogInformation("Cache Hit: Hero Intro");
                return cached;
            }
        }
        var heroDetail = await _context
            .PersonalDetails.Select(p => new
            {
                p.FirstName,
                p.LastName,
                p.Headline,
                p.Summary,
                p.Address,
                p.Email,
                p.Github,
                p.Hackerrank,
                p.Linkedin,
                p.Stackoverflow,
            })
            .FirstOrDefaultAsync();

        var highestQualification = await _context
            .Qualifications.OrderByDescending(q => q.EndDate)
            .Select(q => new { q.Title })
            .FirstOrDefaultAsync();
        var latestSpecializedcert = await _context
            .Certifications.OrderByDescending(c => c.Date)
            .Select(c => new { c.Title })
            .FirstOrDefaultAsync();
        return await _cache.GetOrCreateAsync(
            "Hero_Intro",
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2);

                var data = new HeroDto
                {
                    FirstName = heroDetail!.FirstName,
                    LastName = heroDetail!.LastName,
                    Headline = heroDetail!.Headline,
                    Summary = heroDetail!.Summary,
                    Email = heroDetail!.Email,
                    Github = heroDetail!.Github,
                    Linkedin = heroDetail!.Linkedin,
                    Hackerrank = heroDetail!.Hackerrank,
                    Stackoverflow = heroDetail!.Stackoverflow,
                    HighestQualification = highestQualification!.Title,
                    LatestSpecializedCert = latestSpecializedcert!.Title,
                };
                return data;
            }
        );
    }

    // -----------------------------
    // PersonalDetails (cached)
    // -----------------------------
    public async Task<PersonalDetailDto?> GetPersonalDetailAsync()
    {
        if (_cache.TryGetValue("Portfolio_Personal_Detail", out PersonalDetailDto? cached))
        {
            if (cached != null)
            {
                _logger.LogInformation("Cache Hit: Personal Detail");
                return cached;
            }
            /* _logger.LogInformation("Cache Hit: ");
             return cached;*/
        }
        return await _cache.GetOrCreateAsync(
            "Portfolio_Personal_Detail",
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2);

                var data = await _context.PersonalDetails.FirstOrDefaultAsync();
                return _mapper.Map<PersonalDetailDto?>(data);
            }
        );
    }

    // -----------------------------
    // Certifications (cached)
    // -----------------------------
    public async Task<List<CertificationDto>?> GetCertificationsAsync()
    {
        if (_cache.TryGetValue("Portfolio_Certifications", out List<CertificationDto>? cached))
        {
            if (cached!.Any())
            {
                _logger.LogInformation("Cache Hit: Certification");
                return cached;
            }
            /* _logger.LogInformation("Cache Hit: ");
             return cached;*/
        }
        return await _cache.GetOrCreateAsync(
            "Portfolio_Certifications",
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2);

                var data = await _context
                    .Certifications.OrderByDescending(c => c.Date)
                    .ToListAsync();
                return _mapper.Map<List<CertificationDto>?>(data);
            }
        );
    }

    // -----------------------------
    // Experience (cached)
    // -----------------------------
    public async Task<List<ExpereinceGroupDto>?> GetExperienceAsync()
    {
        if (_cache.TryGetValue("Portfolio_Experience", out List<ExpereinceGroupDto>? cached))
        {
            if (cached!.Any())
            {
                _logger.LogInformation("Cache Hit: Experience");
                return cached;
            }
        }
        return await _cache.GetOrCreateAsync(
            "Portfolio_Experience",
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2);

                var data = await _context
                    .Experiences.GroupBy(e => e.Type)
                    .Select(g => new ExpereinceGroupDto
                    {
                        Type = g.Key,
                        Experiences = g.OrderByDescending(e => e.StartDate) // ⭐ ORDER INSIDE GROUP
                            .Select(e => _mapper.Map<ExperienceDto>(e))
                            .ToList(),
                    })
                    .ToListAsync();
                return data;
            }
        );
    }

    // -----------------------------
    // Skills (cached)
    // -----------------------------
    public async Task<List<SkillAccordionDto>?> GetSkillsAsync()
    {
        if (_cache.TryGetValue("Portfolio_Skills", out List<SkillAccordionDto>? cached))
        {
            if (cached!.Any())
            {
                _logger.LogInformation("Cache Hit: Skill DTO");
                return cached;
            }
            /* _logger.LogInformation("Cache Hit: ");
             return cached;*/
        }
        return await _cache.GetOrCreateAsync(
            "Portfolio_Skills",
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2);

                var data = await _context
                    .SkillAccordions.Include(s => s.Skills)
                    .OrderBy(s => s.DisplayOrder)
                    .ToListAsync();

                return _mapper.Map<List<SkillAccordionDto>>(data);
            }
        );
    }

    // -----------------------------
    // Qualifications (cached)
    // -----------------------------
    public async Task<List<QualificationDto>?> GetQualificationsAsync()
    {
        if (_cache.TryGetValue("Portfolio_Qualifications", out List<QualificationDto>? cached))
        {
            if (cached!.Any())
            {
                _logger.LogInformation("Cache Hit: Qualification");
                return cached;
            }
            /* _logger.LogInformation("Cache Hit: ");
             return cached;*/
        }
        return await _cache.GetOrCreateAsync(
            "Portfolio_Qualifications",
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2);

                var data = await _context.Qualifications.ToListAsync();
                return _mapper.Map<List<QualificationDto>>(data);
            }
        );
    }

    // -----------------------------
    // Projects (cached)
    // -----------------------------
    public async Task<List<ProjectDto>?> GetProjectsAsync()
    {
        if (_cache.TryGetValue("Portfolio_Projects", out List<ProjectDto>? cached))
        {
            if (cached!.Any())
            {
                _logger.LogInformation("Cache Hit: Project");
                return cached;
            }
            /* _logger.LogInformation("Cache Hit: ");
             return cached;*/
        }
        return await _cache.GetOrCreateAsync(
            "Portfolio_Projects",
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2);

                var data = await _context
                    .Projects.Include(p => p.Duties)
                    .Include(p => p.KeySkills)
                        .ThenInclude(ks => ks.Skill)
                    .ToListAsync();

                return _mapper.Map<List<ProjectDto>>(data);
            }
        );
    }
}
