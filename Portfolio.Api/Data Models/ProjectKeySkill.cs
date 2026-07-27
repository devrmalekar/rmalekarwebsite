using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Api.Models;

public class ProjectKeySkill
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int SkillId { get; set; }

    public int ProjectId { get; set; }

    [ForeignKey(nameof(ProjectId))]
    public Project? Project { get; set; }

    [ForeignKey(nameof(SkillId))]
    public Skill? Skill { get; set; }
}
