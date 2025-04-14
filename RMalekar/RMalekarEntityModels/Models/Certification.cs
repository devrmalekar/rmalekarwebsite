using System;
using System.Collections.Generic;

namespace RMalekarEntityModels;

public partial class Certification
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Issuer { get; set; } = null!;

    public string Url { get; set; } = null!;

    public string Img { get; set; } = null!;

    public DateOnly Date { get; set; }
}
