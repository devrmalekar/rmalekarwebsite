using Portfolio.Shared.Dtos;

namespace Portfolio.Api.Interfaces;

public interface IPortfolioService
{
    Task<PortfolioDto?> GetPortfolioAsync();
    Task<HeroDto?> GetHeroDetailAsync();
    Task<PersonalDetailDto?> GetPersonalDetailAsync();
    Task<List<CertificationDto>?> GetCertificationsAsync();
    Task<List<ExpereinceGroupDto>?> GetExperienceAsync();
    Task<List<SkillAccordionDto>?> GetSkillsAsync();
    Task<List<QualificationDto>?> GetQualificationsAsync();
    Task<List<ProjectDto>?> GetProjectsAsync();
}
