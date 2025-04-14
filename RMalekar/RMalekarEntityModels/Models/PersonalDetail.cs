using System;
using System.Collections.Generic;

namespace RMalekarEntityModels;

public partial class PersonalDetail
{
    public sbyte Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Title { get; set; } = null!;

    public int ExperienceYears { get; set; } = 0;

    public string Mobile { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Summary { get; set; } = null!;

    public string Linkedin { get; set; } = null!;

    public string? Github { get; set; }

    public string? Hackerrank { get; set; }
}
