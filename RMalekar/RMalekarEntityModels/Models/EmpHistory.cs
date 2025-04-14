using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace RMalekarEntityModels;

public partial class EmpHistory
{
    public sbyte Id { get; set; }

    public string Position { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public string CompanyAddr { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string Type { get; set; } = null!;

    public string EmpHistoryCode { get; set; } = null!;

    public virtual ICollection<EmpDuty> EmpDuties { get; set; } = new List<EmpDuty>();

    public virtual ICollection<EmpKeySkill> EmpKeySkills { get; set; } = new List<EmpKeySkill>();

    public virtual ICollection<EmpProject> EmpProjects { get; set; } = new List<EmpProject>();
}
