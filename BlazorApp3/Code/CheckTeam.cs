using BlazorApp3.Model;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;

namespace BlazorApp3.Code
{
    public static class CheckTeamApp
    {
        public static bool CheckTeam(Team team)
        {
            if (team.Skip.HasValue && (team.Skip == team.ViceSkip || team.Skip == team.Lead))
                return true;
            if (team.ViceSkip.HasValue && team.ViceSkip == team.Lead)
                return true;
            return false;
        }

        public static async Task OrderTeam(int leagueid)
        {
            using (var _db = new TournamentContext())
            {
                bool changed = false;
                var teams = await _db.Teams.Where(x => x.Leagueid == leagueid).OrderBy(x => x.DivisionId).ToListAsync();
                for (int i = 0; i < teams.Count(); i++)
                {
                    var item = teams[i];
                    if (item.TeamNo != i + 1)
                    {
                        var team = await _db.Teams.FindAsync(item.Id);
                        team.TeamNo = i + 1;
                        changed = true;
                    }
                }
                if (changed)
                    await _db.SaveChangesAsync();
            }
        }
    }
}
