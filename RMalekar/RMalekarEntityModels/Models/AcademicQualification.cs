using System;
using System.Collections.Generic;

namespace RMalekarEntityModels;

public partial class AcademicQualification
{
    public string Qid { get; set; } = null!;

    public string Degree { get; set; } = null!;

    public string? StartDate { get; set; }

    public string? EndDate { get; set; }

    public string Institute { get; set; } = null!;

    public string Addr { get; set; } = null!;

    public string? KeySubjects { get; set; }
}
