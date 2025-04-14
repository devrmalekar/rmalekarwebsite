using System;
using System.Collections.Generic;

namespace RMalekarEntityModels;

public partial class SkillCat
{
    public sbyte Id { get; set; }

    public string CatName { get; set; } = null!;

    public string SkillCat1 { get; set; } = null!;

    public virtual ICollection<SkillSubCat> SkillSubCats { get; set; } = new List<SkillSubCat>();
}
