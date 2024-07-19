using BlazorApp3.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace BlazorApp3.NewModels
{
    public class GetMatchAllTable
    {
        public int? Id {get; set; }
        public string? Skip1 {get; set; }
        public string? Vice1 {get; set; }
        public string? Lead1 {get; set; }
        public string? Skip2 {get; set; }
        public string? Vice2 {get; set; }
        public string? Lead2 {get; set; }
        public string? Team1 {get; set; }
        public string? Team2 {get; set; }
        public DateOnly Date {get; set; }
        public int Rink {get; set; }
        public string? Division1 {get; set; }
        public string? Division2 {get; set; }
    }

    
}
