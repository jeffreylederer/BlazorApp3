using System;
using System.Collections.Generic;

namespace BlazorApp3.Model;

public partial class Membership
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string? Shortname { get; set; }

    public string? NickName { get; set; }

    public bool Wheelchair { get; set; }

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}
