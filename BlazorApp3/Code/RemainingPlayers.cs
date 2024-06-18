using BlazorApp3.Model;




namespace BlazorApp3.Code
{
    public class RemainingPlayers
    {
        public static List<PlayerViewModel> Find(Team team, List<Team> teams )
        {
            var list1 = new List<PlayerViewModel>();
            using (var _db = new TournamentContext())
            {
                var list = new List<IntViewModel>();
                var leagueid = team.Leagueid;
                        

                foreach (var player in _db.Players.Where(x => x.Leagueid == leagueid))
                {
                    if (!teams.Any(x => x.Skip == player.Id || x.Lead == player.Id || x.ViceSkip == player.Id))
                    {
                        list.Add(new IntViewModel()
                        {
                            id = player.Id,
                            memberid = player.MembershipId
                        }); ;
                    }
                }

                if (team.Id != 0)
                {
                    if (team.Skip != null)
                    {
                        list.Add(new IntViewModel()
                        {
                            id = team.Skip.Value,
                            memberid = _db.Players.Where(x => x.Id == team.Skip.Value).First().MembershipId
                        }); ; ;
                    }

                    if (team.ViceSkip != null)
                    {
                        list.Add(new IntViewModel()
                        {
                            id = team.ViceSkip.Value,
                            memberid = _db.Players.Where(x => x.Id == team.ViceSkip.Value).First().MembershipId
                        });
                    }

                    if (team.Lead != null)
                    {
                        list.Add(new IntViewModel()
                        {
                            id = team.Lead.Value,
                            memberid = _db.Players.Where(x => x.Id == team.Lead.Value).First().MembershipId
                        });
                    }
                }
                foreach(var item in list)
                {
                    var member = _db.Memberships.Where(x => x.Id == item.memberid).First();
                    list1.Add(new PlayerViewModel()
                    {
                        id = item.id,
                        FullName = member.FullName,
                        LastName = member.LastName
                    });
                }
                list1.Sort((a, b) => String.Compare(a.LastName, b.LastName, StringComparison.Ordinal));
            }
            return list1;
        }
    }

    

    
}
