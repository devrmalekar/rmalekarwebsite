using System.ComponentModel.DataAnnotations;

namespace Portfolio.ClassicMode.Configuration;

public class ResumeConfig
{
    [Required]
    [Url]
    public string Url { get; set; } = string.Empty;
}
