namespace Portfolio.Shared.Dtos;

public class ExperienceDto
{
    public string Position { get; set; } = null!;
    public string CompanyName { get; set; } = null!;
    public string? CompanyAddress { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string Type { get; set; } = null!;
}
