using System;
using System.Collections.Generic;

namespace BlazorApp3.Model;

public partial class League
{
    public int Id { get; set; }

    public string LeagueName { get; set; } = null!;

    public bool Active { get; set; }

    public int TeamSize { get; set; }

    public bool TiesAllowed { get; set; }

    public bool PointsCount { get; set; }

    public short WinPoints { get; set; }

    public short TiePoints { get; set; }

    public short ByePoints { get; set; }

    public int StartWeek { get; set; }

    public bool PointsLimit { get; set; }

    public short Divisions { get; set; }

    public bool PlayOffs { get; set; }

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual RinkOrder StartWeekNavigation { get; set; } = null!;

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();

    public virtual ICollection<UserLeague> UserLeagues { get; set; } = new List<UserLeague>();
}
