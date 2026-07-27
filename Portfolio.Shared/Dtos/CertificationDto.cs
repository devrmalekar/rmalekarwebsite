namespace Portfolio.Shared.Dtos;

public class CertificationDto
{
    public string Title { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public DateOnly Date { get; set; }
}
