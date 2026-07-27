using System.ComponentModel.DataAnnotations;
using System.Security;

namespace Portfolio.Api.Models;

public class SkillAccordion
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = null!;

    public int DisplayOrder { get; set; }

    public List<Skill> Skills { get; set; } = new();
}
