using System.ComponentModel.DataAnnotations;

namespace Portfolio.Api.Models;

public class SkillCategory
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Name { get; set; } = null!;

    public virtual List<SkillSubCategory>? SkillSubCategories { get; set; } = new();
}
