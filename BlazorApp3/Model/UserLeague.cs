using System;
using System.Collections.Generic;

namespace BlazorApp3.Model;

public partial class UserLeague
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int LeagueId { get; set; }

    public string? Roles { get; set; }

    public virtual League League { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
