namespace Portfolio.Shared.Dtos;

public class ProjectDto
{
    public string Title { get; set; } = null!;
    public string ProjectSummary { get; set; } = null!;
    public string ProjectCode { get; set; } = null!;
    public string Url { get; set; } = null;
    public List<string> Duties { get; set; } = new();
    public List<string> KeySkills { get; set; } = new();
}
