using System;
using System.Collections.Generic;

namespace RMalekarEntityModels;

public partial class KeySubject
{
    public sbyte Id { get; set; }

    public string Ksid { get; set; } = null!;

    public string SubjectTitle { get; set; } = null!;

    public string Qid { get; set; } = null!;

    public virtual Qualification QidNavigation { get; set; } = null!;
}
