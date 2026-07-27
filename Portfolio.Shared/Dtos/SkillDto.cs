using System.Text.Json.Serialization;

namespace Portfolio.Shared.Dtos;

public class SkillDto
{
    public string Name { get; set; } = null!;
    public string? SkillLogo { get; set; }
}
