using AutoMapper;
using Portfolio.Api.Models;
using Portfolio.Shared.Dtos;

public class PortfolioMappingProfile : Profile
{
    public PortfolioMappingProfile()
    {
        // -----------------------------
        // 1) Personal Information
        // -----------------------------
        CreateMap<PersonalDetail, PersonalDetailDto>();

        // -----------------------------
        // 2) Certifications
        // -----------------------------
        CreateMap<Certification, CertificationDto>();

        // -----------------------------
        // 3) Experience
        // -----------------------------
        CreateMap<Experience, ExperienceDto>();

        // -----------------------------
        // 4) Skills (Grouped)
        // -----------------------------

        // Skill → SkillDto
        CreateMap<Skill, SkillDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.SkillLogo, opt => opt.MapFrom(src => src.SkillLogo));

        //SkillAccordion
        CreateMap<SkillAccordion, SkillAccordionDto>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Name));
        // -----------------------------
        // 5) Qualification
        // -----------------------------
        CreateMap<Qualification, QualificationDto>();

        // -----------------------------
        // 6) Projects (Flattened)
        // -----------------------------

        // ProjectDuty → string
        CreateMap<ProjectDuty, string>().ConvertUsing(src => src.Description ?? string.Empty);

        // ProjectKeySkill → string
        CreateMap<ProjectKeySkill, string>().ConvertUsing(src => src.Skill!.Name!);

        // Project → ProjectDto
        CreateMap<Project, ProjectDto>()
            .ForMember(dest => dest.Duties, opt => opt.MapFrom(src => src.Duties))
            .ForMember(dest => dest.KeySkills, opt => opt.MapFrom(src => src.KeySkills));
    }
}
