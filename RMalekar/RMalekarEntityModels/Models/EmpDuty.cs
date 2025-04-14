using System;
using System.Collections.Generic;

namespace RMalekarEntityModels;

public partial class EmpDuty
{
    public sbyte Id { get; set; }

    public string Duties { get; set; } = null!;

    public sbyte? ChildDutyOf { get; set; }

    public string EmpHistoryCode { get; set; } = null!;

    public virtual EmpHistory EmpHistoryCodeNavigation { get; set; } = null!;
}
