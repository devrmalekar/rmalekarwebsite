using System;
using System.Collections.Generic;

namespace RMalekarEntityModels;

public partial class SkillSubCat
{
    public sbyte Id { get; set; }

    public string SubCatName { get; set; } = null!;

    public string? SkillCat { get; set; }

    public string SkillSubcat1 { get; set; } = null!;

    public virtual SkillCat? SkillCatNavigation { get; set; }

    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
}
