using System;
using System.Collections.Generic;

namespace BlazorApp3.Model;

public partial class RinkOrder
{
    public int Id { get; set; }

    public string Green { get; set; } = null!;

    public string Direction { get; set; } = null!;

    public string Boundary { get; set; } = null!;

    public byte[] Rowversion { get; set; } = null!;

    public virtual ICollection<League> Leagues { get; set; } = new List<League>();
}
