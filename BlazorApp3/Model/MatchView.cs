using System;
using System.Collections.Generic;

namespace BlazorApp3.Model;

public partial class MatchView
{
    public int Id { get; set; }

    public string? Skip1 { get; set; }

    public string? Vice1 { get; set; }

    public string? Lead1 { get; set; }

    public string? Skip2 { get; set; }

    public string? Vice2 { get; set; }

    public string? Lead2 { get; set; }

    public int TeamNo1 { get; set; }

    public int TeamNo2 { get; set; }

    public int Rink { get; set; }

    public int Team1Id { get; set; }

    public int Team2Id { get; set; }

    public int Team1Score { get; set; }

    public int Team2Score { get; set; }

    public int ForFeitId { get; set; }

    public int Weekid { get; set; }

    public bool? Skip1W { get; set; }

    public bool? Vice1W { get; set; }

    public bool? Lead1W { get; set; }

    public bool? Skip2W { get; set; }

    public bool? Vice2W { get; set; }

    public bool? Lead2W { get; set; }
}
