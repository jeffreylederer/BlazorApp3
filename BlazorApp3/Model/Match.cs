using System;
using System.Collections.Generic;

namespace BlazorApp3.Model;

public partial class Match
{
    public int Id { get; set; }

    public int WeekId { get; set; }

    public int Rink { get; set; }

    public int TeamNo1 { get; set; }

    public int? TeamNo2 { get; set; }

    public int Team1Score { get; set; }

    public int Team2Score { get; set; }

    public int ForFeitId { get; set; }

    public virtual Team TeamNo1Navigation { get; set; } = null!;

    public virtual Team? TeamNo2Navigation { get; set; }

    public virtual Schedule Week { get; set; } = null!;
}
