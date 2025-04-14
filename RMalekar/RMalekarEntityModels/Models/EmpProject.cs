using System;
using System.Collections.Generic;

namespace RMalekarEntityModels;

public partial class EmpProject
{
    public sbyte Id { get; set; }

    public string ProjectTitle { get; set; } = null!;

    public string ProjectSummary { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string EmpHistoryCode { get; set; } = null!;

    public virtual EmpHistory EmpHistoryCodeNavigation { get; set; } = null!;
}
