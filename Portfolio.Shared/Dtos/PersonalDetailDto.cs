namespace Portfolio.Shared.Dtos;

public class PersonalDetailDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public int ExperienceYears { get; set; }
    public string Mobile { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Headline { get; set; } = null!;
    public string Summary { get; set; } = null!;
    public string Introduction { get; set; } = null!;
    public string Stackoverflow { get; set; } = null!;
    public string? Github { get; set; }
    public string? Hackerrank { get; set; }
    public string? Linkedin { get; set; }
}
