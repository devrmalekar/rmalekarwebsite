using System.ComponentModel.DataAnnotations;

namespace Portfolio.Api.Models;

public class PersonalDetail
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string FirstName { get; set; } = null!;

    [Required, MaxLength(50)]
    public string LastName { get; set; } = null!;

    [Range(0, 50)]
    public int ExperienceYears { get; set; } = 0;

    [Required, MaxLength(20)]
    public string Mobile { get; set; } = null!;

    [Required, EmailAddress, MaxLength(100)]
    public string Email { get; set; } = null!;

    [Required, MaxLength(200)]
    public string Address { get; set; } = null!;

    [Required, MaxLength(800)]
    public string Summary { get; set; } = null!;

    [Required, MaxLength(500)]
    public string Headline { get; set; } = null;

    [Required, MaxLength(3000)]
    public string Introduction { get; set; } = null;

    [Required, MaxLength(200)]
    public string Stackoverflow { get; set; } = null!;

    [MaxLength(200)]
    public string? Github { get; set; }

    [MaxLength(200)]
    public string? Hackerrank { get; set; }

    [MaxLength(200)]
    public string? Linkedin { get; set; }
}
