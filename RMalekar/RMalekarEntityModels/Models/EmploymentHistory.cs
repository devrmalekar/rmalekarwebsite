using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMalekarEntityModels;


public partial class EmploymentHistory
{
    public string Position { get; set; } = null!;

    [Column(name: "company_name")]
    public string CompanyName { get; set; } = null!;

    [Column(name: "company_addr")]
    public string CompanyAddr { get; set; } = null!;

    [Column(name: "start_date")]
    public string StartDate { get; set; }

    [Column(name: "end_date")]
    public string? EndDate { get; set; }

    public string? Duties { get; set; }

    [Column(name: "key_skills")]
    public string? KeySkills { get; set; }

    [Column(name: "project_title")]
    public string? ProjectTitle { get; set; }

    [Column(name: "project_summary")]
    public string? ProjectSummary { get; set; }

    public string Type { get; set; } = null!;
}
