using System;
using System.Collections.Generic;

namespace RMalekarEntityModels;

public partial class ProjectDuty
{
    public sbyte Id { get; set; }

    public string DutiesDesc { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;
}
