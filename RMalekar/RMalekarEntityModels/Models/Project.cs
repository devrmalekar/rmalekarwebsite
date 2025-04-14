using System;
using System.Collections.Generic;

namespace RMalekarEntityModels;

public partial class Project
{
    public sbyte Id { get; set; }

    public string ProjectTitle { get; set; } = null!;

    public string ProjectSummary { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual ICollection<ProjectDuty> ProjectDuties { get; set; } = new List<ProjectDuty>();

    public virtual ICollection<ProjectKeySkill> ProjectKeySkills { get; set; } = new List<ProjectKeySkill>();
}
