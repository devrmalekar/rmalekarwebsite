using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Api.Models;

public class Qualification
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(5)]
    public string Qid { get; set; } = null!;

    [Required, MaxLength(100)]
    public string Title { get; set; } = null!;

    [Required]
    public DateOnly StartDate { get; set; }

    [Required]
    public DateOnly EndDate { get; set; }

    [Required, MaxLength(50)]
    public string Institute { get; set; } = null!;

    [Required, MaxLength(5)]
    public string GPA { get; set; } = null!;

    [Required, MaxLength(100)]
    public string Address { get; set; } = null!;
}
