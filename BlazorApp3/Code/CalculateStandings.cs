using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using BlazorApp3.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace BlazorApp3.Code
{
    public static class CalculateStandings
    {

        
        /// <summary>
        /// Determine the standings based on all the games played so far
        /// </summary>
        /// <param name="weekid">the record id of the week in the schedule table</param>
        /// <param name="teamsize">number of players per team</param>
        /// <param name="leagueid">the record id of the league table</param>
        /// <returns></returns>
        public static TournamentDS.StandDataTable Doit(int weekid, League league)
        {
            int teamsize = league.TeamSize;
            var ds = new TournamentDS();
            var list = new List<Standing>();
            using (var db = new TournamentEntities())
            {
                // get the names of the player for each team
                foreach (var team in db.Teams.Where(x=>x.Leagueid==league.id))
                {
                    string players = "";
                    switch (teamsize)
                    {
                        case 1:
                            players = team.Player.Membership.NickName;
                            break;
                        case 2:
                            players = $"{team.Player.Membership.NickName}, {team.Player2.Membership.NickName}";
                            break;
                        case 3:
                            players = $"{team.Player.Membership.NickName}, {team.Player1.Membership.NickName}, {team.Player2.Membership.NickName}";
                            break;
                    }
                    list.Add(new Standing()
                    {
                        TeamNumber = team.TeamNo,
                        Wins = 0,
                        Loses = 0,
                        TotalScore = 0,
                        Ties = 0,
                        Byes = 0,
                        Players = players,
                        DivisionId = team.DivisionId
                    });
                }

                // determine the total score and wins and loses for each team for each week
                foreach(var week in db.Schedules.Where(x => x.id <= weekid && x.Leagueid == league.id))
                {

                    //cancelled weeks do not count
                    if (week.Cancelled)
                        continue;
                    //var total = 0;
                    var numMatches = 0;
                    var bye = false;
                    bool forfeit = false;
                    foreach (var match in db.Matches.Where(x => x.WeekId == week.id))
                    {
                        // both teams forfeits
                        if (match.Rink != -1 && match.ForFeitId == -1)
                        {
                            var winner = list.Find(x => x.TeamNumber == match.Team.TeamNo);
                            var loser = list.Find(x => x.TeamNumber == match.Team1.TeamNo);
                            winner.Loses++;
                            loser.Loses++;
                           
                        }
                        // tie game
                        else if (match.Team1Score == match.Team2Score && match.Rink != -1 && match.ForFeitId == 0)
                        {
                            var winner = list.Find(x => x.TeamNumber == match.Team.TeamNo);
                            var loser = list.Find(x => x.TeamNumber == match.Team1.TeamNo);
                            winner.Ties++;
                            loser.Ties++;
                            if (league.PointsLimit)
                            {
                                winner.TotalScore += Math.Min(20, match.Team1Score);
                                loser.TotalScore += Math.Min(20, match.Team2Score);
                                //total += Math.Min(20, match.Team1Score);
                            }
                            else
                            {
                                winner.TotalScore += match.Team1Score;
                                loser.TotalScore += match.Team2Score;
                                //total += match.Team1Score;
                            }
                            numMatches++;
                        }
                        //team 1 wins
                        else if (match.Team1Score > match.Team2Score && match.Rink != -1 && match.ForFeitId == 0)
                        {
                            var winner = list.Find(x => x.TeamNumber == match.Team.TeamNo);
                            var loser = list.Find(x => x.TeamNumber == match.Team1.TeamNo);
                            winner.Wins++;
                            loser.Loses++;
                            if (league.PointsLimit)
                            {
                                winner.TotalScore += Math.Min(20, match.Team1Score);
                                loser.TotalScore += Math.Min(20, match.Team2Score);
                        }
                            else
                            {
                                winner.TotalScore += match.Team1Score;
                                loser.TotalScore += match.Team2Score;
                            }
                            numMatches++;
                        }
                        //team 2 wins
                        else if (match.Rink != -1 && match.ForFeitId == 0)
                        {
                            var winner = list.Find(x => x.TeamNumber == match.Team1.TeamNo);
                            var loser = list.Find(x => x.TeamNumber == match.Team.TeamNo);
                            winner.Wins++;
                            loser.Loses++;
                            if (league.PointsLimit)
                            {
                                winner.TotalScore += Math.Min(20, match.Team2Score);
                                loser.TotalScore += Math.Min(20, match.Team1Score);
                            }
                            else
                            {
                                winner.TotalScore += match.Team2Score;
                                loser.TotalScore += match.Team1Score;
                            }
                            numMatches++;
                        }
                        // one team forfeits
                        else if (match.Rink != -1 && match.ForFeitId > 0)
                        {
                            var winner = list.Find(x => x.TeamNumber == (match.Team.TeamNo== match.ForFeitId? match.Team1.TeamNo: match.Team.TeamNo));
                            var loser = list.Find(x => x.TeamNumber == match.ForFeitId);
                            forfeit = true;
                            winner.Wins++;
                            loser.Loses++;
                        }
                        //bye
                        else
                        {
                            var winner = list.Find(x => x.TeamNumber == match.Team.TeamNo);
                            winner.Byes++;
                            bye = true;
                        }

                    }

                    // for byes or forfeit (the team that did not forfeit), the team gets the average score of all winning games that week and a win
                    if (bye || forfeit)
                    {

                        foreach (var match in db.Matches.Where(x => x.WeekId == week.id))
                        {
                            if (match.Rink != -1 && match.ForFeitId > 0)
                            {
                                var winner = list.Find(x => x.TeamNumber == (match.Team.TeamNo == match.ForFeitId ? match.Team1.TeamNo : match.Team.TeamNo));
                                winner.TotalScore += 14;
                            }
                            else if (match.Rink == -1 && match.ForFeitId !=-1)
                            {
                                var winner = list.Find(x => x.TeamNumber == match.Team.TeamNo);
                                winner.TotalScore += 14;
                            }
                        }
                    }



                }
            }
            int place = 1;
            int nextplace = 1;
            Standing previous = new Standing()
            {
                Loses = 0,
                TotalScore = 0,
                Wins = 0,
                Ties = 0,
                Byes = 0
                
            };
            foreach (var item in list)
            {
                var points = item.Wins * league.WinPoints + 
                                   item.Ties * league.TiePoints + item.Byes * league.ByePoints;
                if (league.PointsCount)
                    item.TotalPoints = points * 1000 + item.TotalScore;
                else
                {
                    item.TotalPoints = points;
                    item.TotalScore = points;
                }

            }
            ds.Stand.Clear();
            list.Sort((a, b) => (b.TotalPoints).CompareTo(a.TotalPoints));
            foreach (var item in list)
            {
                if (item.TotalPoints != previous.TotalPoints)
                {
                    place = nextplace;
                }
                ds.Stand.AddStandRow(item.TeamNumber,item.Players, item.TotalScore, place, item.Wins, item.Loses, item.Ties, item.Byes, item.DivisionId);
                previous = item;
                nextplace++;
            }
            return ds.Stand;
        }
    }


    internal class Standing
    {
        public int TeamNumber { get; set; }
        public int Wins { get; set; }
        public int Loses { get; set; }
        public int TotalScore { get; set; }
        public string Players { get; set; }
        public int Ties { get; set; }
        public int Byes { get; set; }
        public short DivisionId { get; set; }
       
        public int TotalPoints { get; set; }

        
        
        
    }
}