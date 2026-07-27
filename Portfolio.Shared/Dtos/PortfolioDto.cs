namespace Portfolio.Shared.Dtos;

public class PortfolioDto
{
    public PersonalDetailDto Personal { get; set; } = null!;
    public List<CertificationDto> Certifications { get; set; } = new();
    public List<ExperienceDto> Experience { get; set; } = new();
    public List<SkillDto> Skills { get; set; } = new();
    public List<QualificationDto> Qualifications { get; set; } = new();
    public List<ProjectDto> Projects { get; set; } = new();
}
