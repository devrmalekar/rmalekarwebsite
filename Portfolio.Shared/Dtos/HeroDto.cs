namespace Portfolio.Shared.Dtos;

public class HeroDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Headline { get; set; } = null!;
    public string Summary { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string HighestQualification { get; set; } = null!;
    public string LatestSpecializedCert { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Stackoverflow { get; set; } = null!;
    public string? Github { get; set; }
    public string? Hackerrank { get; set; }
    public string? Linkedin { get; set; }
}
