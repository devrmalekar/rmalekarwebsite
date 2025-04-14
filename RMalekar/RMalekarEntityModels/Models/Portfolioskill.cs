using System;
using System.Collections.Generic;

namespace RMalekarEntityModels;

public partial class Portfolioskill
{
    public sbyte Id { get; set; }

    public string PortfolioId { get; set; } = null!;

    public sbyte SkillId { get; set; }
}
