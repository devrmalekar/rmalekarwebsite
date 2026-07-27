using System.ComponentModel.DataAnnotations;

namespace Portfolio.ClassicMode.Configuration;

public class ApiSettings
{
    [Required]
    [Url]
    public string BaseUrl { get; set; } = string.Empty;
}
