using System;
using System.Collections.Generic;

namespace RMalekarEntityModels;

public partial class AcademicProject
{
    public string ProjectTitle { get; set; } = null!;

    public string ProjectSummary { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string? Skills { get; set; }

    public string? Duties { get; set; }
}
