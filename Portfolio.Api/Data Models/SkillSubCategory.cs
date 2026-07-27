using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Api.Models;

public class SkillSubCategory
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Name { get; set; } = null!;

    [Required]
    public int SkillCategoryId { get; set; }

    public List<Skill>? Skills { get; set; }

    [ForeignKey(nameof(SkillCategoryId))]
    public SkillCategory? SkillCategory { get; set; }
}
