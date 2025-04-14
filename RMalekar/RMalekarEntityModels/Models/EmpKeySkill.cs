using System;
using System.Collections.Generic;

namespace RMalekarEntityModels;

public partial class EmpKeySkill
{
    public sbyte Id { get; set; }

    public sbyte SkillId { get; set; }

    public string EmpHistoryCode { get; set; } = null!;

    public virtual EmpHistory EmpHistoryCodeNavigation { get; set; } = null!;
}
