using System;
using System.Collections.Generic;

namespace RMalekarEntityModels;

public partial class PortfolioItem
{
    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string CodeLink { get; set; } = null!;

    public string Thumbnail { get; set; } = null!;

    public string? Skills { get; set; }
}
