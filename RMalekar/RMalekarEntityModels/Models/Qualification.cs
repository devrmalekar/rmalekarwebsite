using System;
using System.Collections.Generic;

namespace RMalekarEntityModels;

public partial class Qualification
{
    public sbyte Id { get; set; }

    public string Qid { get; set; } = null!;

    public string Title { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public string Institute { get; set; } = null!;

    public string Addr { get; set; } = null!;

    public virtual ICollection<KeySubject> KeySubjects { get; set; } = new List<KeySubject>();
}
