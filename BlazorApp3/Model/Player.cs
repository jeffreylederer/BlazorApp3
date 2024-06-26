using System;
using System.Collections.Generic;

namespace BlazorApp3.Model;

public partial class Player
{
    public int Id { get; set; }

    public int Leagueid { get; set; }

    public int MembershipId { get; set; }

    public virtual League League { get; set; } = null!;

    public virtual Membership Membership { get; set; } = null!;

    public virtual ICollection<Team> TeamLeadNavigations { get; set; } = new List<Team>();

    public virtual ICollection<Team> TeamSkipNavigations { get; set; } = new List<Team>();

    public virtual ICollection<Team> TeamViceSkipNavigations { get; set; } = new List<Team>();
}
