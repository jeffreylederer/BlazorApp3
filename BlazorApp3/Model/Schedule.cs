using System;
using System.Collections.Generic;

namespace BlazorApp3.Model;

public partial class Schedule
{
    public int Id { get; set; }

    public DateOnly GameDate { get; set; }

    public int Leagueid { get; set; }

    public byte[] Rowversion { get; set; } = null!;

    public bool Cancelled { get; set; }

    public string? WeekDate { get; set; }

    public bool PlayOffs { get; set; }

    public virtual League League { get; set; } = null!;

    public virtual ICollection<Match> Matches { get; set; } = new List<Match>();
}
