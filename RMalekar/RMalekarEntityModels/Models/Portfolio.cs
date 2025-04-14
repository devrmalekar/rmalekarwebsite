using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMalekarEntityModels;

[Table(name:"Portfolio")]
public partial class Portfolio
{
    public sbyte Id { get; set; }

    public string PortfolioId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string CodeLink { get; set; } = null!;

    public string Thumbnail { get; set; } = null!;

    public virtual ICollection<Portfolioskill> Portfolioskills { get; set; } = new List<Portfolioskill>();
}
