using System;
using System.Collections.Generic;

namespace BlazorApp3.Model;

public partial class TeamsView
{
    public int Id { get; set; }

    public string? Skip { get; set; }

    public string? ViceSkip { get; set; }

    public string? Lead { get; set; }

    public int Leagueid { get; set; }

    public short DivisionId { get; set; }

    public int TeamNo { get; set; }
}
