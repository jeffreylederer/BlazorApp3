using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;



namespace BlazorApp3.Code
{
    public class CreateSchedule
    {
        private const int BYE = -1;

        /// <summary>
        /// It uses a week robin algoritm to generate the schedule.
        /// </summary>
        /// <param name="Weeks">number of weeks for this league</param>
        /// <param name="numTeams">number of teams in this league</param>
        /// <returns>Generates a list of matches. Each list has a week number, rink number, team 1 number and team 2 number. If the
        /// number of teams are odd, a bye week game is created with a rink of -1 and both teams are the same.</returns>
        public List<CalculatedMatch> RoundRobin(int Weeks, int numTeams)
        {
           
            List<CalculatedMatch> schedule = new List<CalculatedMatch>();
            // Create a list of teams
            List<int> teams = new List<int>();
            for (int i = 0; i < numTeams; i++)
            {
                teams.Add(i);
            }

            // If the number of teams is odd, add a dummy team
            if (numTeams % 2 != 0)
            {
                teams.Add(-1); // Dummy team represented by -1
                numTeams++;
            }

            
            // Generate schedule for each week
            for (int week = 0; week < Weeks; week++)
            {
                int rink = 0;
                for (int i = 0; i < numTeams / 2; i++)
                {
                    int team1 = teams[i];
                    int team2 = teams[numTeams - 1 - i];

                    // Skip dummy team matches
                    if (team1 != -1 && team2 != -1)
                    {
                        schedule.Add(new CalculatedMatch()
                        {
                            Week = week,
                            Rink = rink++,
                            Team1 = team1,
                            Team2 = team2
                        });
                    }
                    else if (team1 == -1)
                    {
                        schedule.Add(new CalculatedMatch()
                        {
                            Week = week,
                            Rink = -1,
                            Team1 = team2,
                            Team2 = team2
                        });
                    }
                    else
                    {
                        schedule.Add(new CalculatedMatch()
                        {
                            Week = week,
                            Rink = -1,
                            Team1 = team1,
                            Team2 = team1
                        });
                    }
                }

                // Rotate teams for next week
                teams.Insert(1, teams[numTeams - 1]);
                teams.RemoveAt(numTeams);
            }
            return schedule;
        }

        /// <summary>
        /// This is called to generate matches for leagues with multiple divisions. It will fill in weeks after the week robin play with
        /// inter divisional matches. If the number of teams is not divisible by 4, it will schedule inter divisional matches on bye weeks.
        /// </summary>
        /// <param name="weeks">number of weeks not counting playoffs</param>
        /// <param name="numberOfTeams">number of teams in the league. It must be divisible by 2</param>
        /// <returns>Generates a list of matches. Each list has a week number, rink number, team 1 number and team 2 number.
        /// </returns>
        public List<CalculatedMatch> matchesWithDivisions(int weeks, int numberOfTeams)
        {
            var list = RoundRobin(numberOfTeams / 2, numberOfTeams / 2);
            var list1 = new List<CalculatedMatch>();
          

            // create matches for teams in division 2 using matches in division 1
            var numberOfRinks = numberOfTeams / 4;
            foreach (var match in list)
            {
                if (match.Rink > -1)
                {
                    var match1 = new CalculatedMatch()
                    {
                        Week = match.Week,
                        Rink = match.Rink + numberOfRinks,
                        Team1 = match.Team1 + numberOfTeams / 2,
                        Team2 = match.Team2 + numberOfTeams / 2
                    };
                    list1.Add(match1);
                }

            }

            // handle the bye weeks with inter divisional games.
            var newList = list.FindAll(x => x.Rink == -1);
            foreach (var item in newList)
            {
                int index = list.IndexOf(item);
                var item1 = list[index];
                item1.Rink = numberOfTeams / 2 - 1;
                item1.Team2 += numberOfTeams / 2;
                list[index] = item1;
            }

            //file in the rest of the schedule with inter divisional matches
            for (int w = numberOfTeams / 2; w < weeks; w++)
            {
                for (int i = 0; i < numberOfTeams / 2; i++)
                {
                    int team2 = (i + w + 1);
                    if (team2 >= numberOfTeams)
                        team2 = team2 - numberOfTeams / 2;
                    var match1 = new CalculatedMatch()
                    {
                        Week = w,
                        Rink = i,
                        Team1 = i,
                        Team2 = team2
                    };
                    list1.Add(match1);
                }
            }
            foreach (var item in list1)
                list.Add(item);

            list.Sort((a, b) => (a.Week * 100 + a.Rink).CompareTo(b.Week * 100 + b.Rink));
            return list;
        }
    }

    public class CalculatedMatch
    {
        public int Week;
        public int Rink;
        public int Team1;
        public int Team2;

    }
}