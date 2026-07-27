namespace Portfolio.Shared.Dtos;

public class SkillAccordionDto
{
    public string Category { get; set; } = null!;

    public int DisplayOrder { get; set; }

    public List<SkillDto> Skills { get; set; } = null;
}
