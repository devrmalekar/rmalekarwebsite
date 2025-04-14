using System;
using System.Collections.Generic;

namespace RMalekarEntityModels;

public partial class Allskill
{
    public string Skill { get; set; } = null!;

    public string SkillType { get; set; } = null!;

    public string? SkillLogo { get; set; }

    public string SubCatName { get; set; } = null!;

    public string CatName { get; set; } = null!;
}
