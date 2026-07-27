namespace Portfolio.Shared.Dtos;

public class SkillSubCategoryDto
{
    public string SubCategory { get; set; } = null!;
    public List<SkillDto> Skills { get; set; } = new();
}
