using System.ComponentModel.DataAnnotations;

namespace Portfolio.Api.Models;

public class Project
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(300)]
    public string Title { get; set; } = null!;

    [Required, MaxLength(500)]
    public string ProjectSummary { get; set; } = null!;

    [Required, MaxLength(10)]
    public string ProjectCode { get; set; } = null!;

    [Required]
    public string Url { get; set; } = null;

    public List<ProjectDuty> Duties { get; set; } = new();

    public List<ProjectKeySkill> KeySkills { get; set; } = new();
}
