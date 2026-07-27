using System.ComponentModel.DataAnnotations;

namespace Portfolio.Api.Models;

public class Certification
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Title { get; set; } = null!;

    [Required, MaxLength(100)]
    public string Issuer { get; set; } = null!;

    [Required, MaxLength(200)]
    public string Url { get; set; } = null!;

    [Required, MaxLength(200)]
    public string ImageUrl { get; set; } = null!;

    [Required]
    public DateOnly Date { get; set; }
}
