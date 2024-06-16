using System;
using System.Collections.Generic;

namespace BlazorApp3.Model;

public partial class UserLeagueView
{
    public int Id { get; set; }

    public string? LeagueName { get; set; }

    public string Username { get; set; } = null!;

    public string? LeagueRole { get; set; }

    public string? SiteRole { get; set; }

    public bool? Active { get; set; }
}
