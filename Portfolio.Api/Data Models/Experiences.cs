using System.ComponentModel.DataAnnotations;

namespace Portfolio.Api.Models;

public class Experience
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Position { get; set; } = null!;

    [Required, MaxLength(200)]
    public string CompanyName { get; set; } = null!;

    [MaxLength(200)]
    public string? CompanyAddress { get; set; }

    [Required]
    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    // Relevant / Irrelevant
    [Required, MaxLength(20)]
    public string Type { get; set; } = null!;
}
