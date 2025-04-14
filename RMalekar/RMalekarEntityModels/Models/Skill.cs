using System;
using System.Collections.Generic;

namespace RMalekarEntityModels;

public partial class Skill
{
    public sbyte Id { get; set; }

    public string Skill1 { get; set; } = null!;

    public string SkillType { get; set; } = null!;

    public string? SkillLogo { get; set; }

    public string SkillSubcat { get; set; } = null!;

    public virtual ICollection<ProjectKeySkill> ProjectKeySkills { get; set; } = new List<ProjectKeySkill>();

    public virtual SkillSubCat SkillSubcatNavigation { get; set; } = null!;
}
