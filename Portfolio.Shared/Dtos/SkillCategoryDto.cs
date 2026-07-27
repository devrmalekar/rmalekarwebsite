namespace Portfolio.Shared.Dtos;

public class SkillCategoryDto
{
    public string Category { get; set; } = null!;
    public List<SkillSubCategoryDto> SubCategories { get; set; } = new();
}
