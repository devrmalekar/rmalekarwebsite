namespace Portfolio.Shared.Dtos;

public class QualificationDto
{
    public string Qid { get; set; } = null!;
    public string Title { get; set; } = null!;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public string Institute { get; set; } = null!;
    public string Address { get; set; } = null!;
}
