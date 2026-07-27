using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Api.Models;

public class Skill
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string? Name { get; set; }

    public string? SkillLogo { get; set; }

    [Required]
    public int SkillAccordionId { get; set; }

    [Required, ForeignKey(nameof(SkillAccordionId))]
    public SkillAccordion? SkillAccordion { get; set; }
}
