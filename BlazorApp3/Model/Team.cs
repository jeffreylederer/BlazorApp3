using System;
using System.Collections.Generic;

namespace BlazorApp3.Model;

public partial class Team
{
    public int Id { get; set; }

    public int? Skip { get; set; }

    public int? ViceSkip { get; set; }

    public int? Lead { get; set; }

    public int Leagueid { get; set; }

    public int TeamNo { get; set; }

    public short DivisionId { get; set; }

    public virtual Player? LeadNavigation { get; set; }

    public virtual League League { get; set; } = null!;

    public virtual ICollection<Match> MatchTeamNo1Navigations { get; set; } = new List<Match>();

    public virtual ICollection<Match> MatchTeamNo2Navigations { get; set; } = new List<Match>();

    public virtual Player? SkipNavigation { get; set; }

    public virtual Player? ViceSkipNavigation { get; set; }
}
