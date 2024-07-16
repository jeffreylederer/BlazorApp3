using BlazorApp3.Model;

namespace BlazorApp3.Code
{
    public class UpdateMatches
    {
        public string? FullName { get; set; }

        public static string Create(League league)
        {
            int leagueid = league.Id;
            using (var DB = new TournamentContext())
            {
                var numOfWeeks = DB.Schedules.Count(x => x.Leagueid == leagueid);
                var numofTeams = DB.Teams.Count(x => x.Leagueid == leagueid);

              

                if (league.Divisions > 1)
                {
                    if (DB.Teams.Where(x => x.Leagueid == leagueid).Count() % 2 == 1)
                    {
                        return "League with divisions must have an even number of teams";
                    }
                    var divisionSize = DB.Teams.Where(x => x.Leagueid == leagueid && x.DivisionId == 1).Count();
                    for (int j = 2; j <= league.Divisions; j++)
                    {
                        if (DB.Teams.Where(x => x.Leagueid == leagueid && x.DivisionId == j).Count() != divisionSize)
                        {
                            return "Each division in a league must be the same size";
                        }
                    }
                }

                var matches = DB.Matches.Join(
                    DB.Schedules,
                    m => m.WeekId,
                    s => s.Id,
                    (m, s) => new
                    {
                        
                        LeagueId = s.Leagueid,
                        match = m
                    }).Where(x => x.LeagueId == leagueid);
                foreach (var match in matches)
                {
                    if (match.match.Team1Score != 0 || match.match.Team2Score != 0 || match.match.ForFeitId != 0)
                    {
                        return "Matches cannot be delete, some matches have scores";
                    }
                }
                List <Match> newList = new List<Match>();
                foreach (var match in matches)
                    newList.Add(match.match);
                DB.Matches.RemoveRange(newList);


                var cs = new CreateSchedule();
                List<CalculatedMatch>? newMatches = null;
                if (league.Divisions > 1)
                    newMatches = cs.matchesWithDivisions(DB.Schedules.Where(x => !x.PlayOffs).Count(x => x.Leagueid == leagueid), numofTeams);
                else
                    newMatches = cs.RoundRobin(numOfWeeks, numofTeams);




                var scheduleList = DB.Schedules.Where(x => x.Leagueid == leagueid).ToList();
                var lookup = new Dictionary<int, DateOnly>();
                int i = 1;
                foreach (var item in scheduleList)
                {
                    lookup[i++] = item.GameDate;
                }
                var teamList = DB.Teams.Where(x => x.Leagueid == leagueid).ToList();

                foreach (var match in newMatches)
                {
                    var team1 = teamList.Find(x => x.TeamNo == match.Team1 + 1);
                    var team2 = teamList.Find(x => x.TeamNo == match.Team2 + 1);
                    var date = lookup[match.Week + 1];
                    var round = scheduleList.Find(x => x.GameDate == date);
                    if (!round.PlayOffs)
                    {
                        DB.Matches.Add(new Match()
                        {
                            Id = 0,
                            WeekId = round.Id,
                            Rink = match.Rink == -1 ? -1 : match.Rink + 1,
                            TeamNo1 = team1.Id,
                            TeamNo2 = match.Rink == -1 ? (int?)null : team2.Id,
                            Team1Score = 0,
                            Team2Score = 0,
                            ForFeitId = 0
                        });
                    }
                }
                try
                {
                    DB.SaveChanges();
                }
                catch (Exception e)
                {
                    return $"No matches were created, Error {e.Message}";
                }
                var rounds = DB.Schedules.Where(x => x.Leagueid == leagueid);
                if (!rounds.Any())
                {
                    return "No matches created because no weeks have been scheduled";
                }

            }
            return "";
        }


        public static string Check(League league)
        {
            using (var DB = new TournamentContext())
            {
                var matches = DB.Matches.Join(
                   DB.Schedules,
                   m => m.WeekId,
                   s => s.Id,
                   (m, s) => new
                   {

                       LeagueId = s.Leagueid,
                       match = m
                   }).Where(x => x.LeagueId == league.Id);

                foreach (var match in matches)
                {
                    if ((match.match.Team1Score + match.match.Team2Score + match.match.ForFeitId) > 0)
                    {
                        return "Matches cannot be delete, some matches have scores";
                    }


                }
                if (!Complete(league.Id, DB))
                    return "Not all teams are complete";
            }
            return "";
        }

        private static bool Complete(int leagueid, TournamentContext DB)
        {
            var teamsize = DB.Leagues.Find(leagueid).TeamSize;
            var complete = true;
            foreach (var team in DB.Teams.Where(x => x.Leagueid == leagueid).ToList())
            {
                switch (teamsize)
                {
                    case 1:
                        if (!team.Skip.HasValue)
                        {
                            complete = false;
                        }
                        break;

                    case 2:
                        if (!team.Skip.HasValue || !team.Lead.HasValue)
                        {
                            complete = false;
                        }
                        break;
                    case 3:
                        if (!team.Skip.HasValue || !team.Lead.HasValue || !team.ViceSkip.HasValue)
                        {
                            complete = false;
                        }
                        break;
                }
                if (!complete)
                    break;

            }
                
            return complete;
        }
            

        public static string Delete(League league)
        {
            using (var DB = new TournamentContext())
            {
                var matches = DB.Matches.Join(
                   DB.Schedules,
                   m => m.WeekId,
                   s => s.Id,
                   (m, s) => new
                   {

                       LeagueId = s.Leagueid,
                       match = m
                   }).Where(x => x.LeagueId == league.Id);
                List<Match> newList = new List<Match>();
                foreach (var match in matches)
                {
                    newList.Add(match.match);
                }
                try
                {
                    DB.Matches.RemoveRange(newList);
                    DB.SaveChanges();
                }
                catch (Exception e)
                {

                    return $"Matches were not removed, Error: {e.Message}";
                  
                }
            }
            return "";
        }

        public static IQueryable<UpdateMatches> Missing(int leagueid)
        {
            List<UpdateMatches> missing = new List<UpdateMatches>();
            using (var DB = new TournamentContext())
            {
                var playerList = DB.Players.Where(x => x.Leagueid == leagueid).ToList();
                foreach (var team in DB.Teams.Where(x => x.Leagueid == leagueid))
                {
                    if (team.Skip.HasValue && playerList.Find(x => x.Id == team.Skip.Value) != null)
                    {
                        playerList.Remove(playerList.Find(x => x.Id == team.Skip.Value));
                    }
                    if (team.Lead.HasValue && playerList.Find(x => x.Id == team.Lead.Value) != null)
                    {
                        playerList.Remove(playerList.Find(x => x.Id == team.Lead.Value));
                    }
                    if (team.ViceSkip.HasValue && playerList.Find(x => x.Id == team.ViceSkip.Value) != null)
                    {
                        playerList.Remove(playerList.Find(x => x.Id == team.ViceSkip.Value));
                    }

                }
                
                foreach (var player in playerList)
                {
                    missing.Add(new UpdateMatches()
                    {
                        FullName = DB.Memberships.Find(player.MembershipId).FullName
                    });
                }
            }
            return missing.AsQueryable();
        }       
    }

    
}
