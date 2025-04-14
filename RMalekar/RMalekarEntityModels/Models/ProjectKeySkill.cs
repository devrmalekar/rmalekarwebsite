using System;
using System.Collections.Generic;

namespace RMalekarEntityModels;

public partial class ProjectKeySkill
{
    public sbyte Id { get; set; }

    public sbyte SkillId { get; set; }

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public virtual Skill Skill { get; set; } = null!;
}
