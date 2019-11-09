using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FootbalMatchFixtureAlgo
{
    class Program
    {
        public static void Main(string[] args)
        {

            string gameStart = "31/10/2019";

            DateTime gameStartDate;
            if (DateTime.TryParseExact(gameStart, "mm/dd/yyyy", null, DateTimeStyles.None, out gameStartDate) == true)
            {
                List<string> gameFixtures = GenerateMatchFixtures(GetFootballTeam());
                ScheduleFootballMatch(gameFixtures, gameStartDate);
            }
            else
            {
                throw new Exception("Invalid Date");
            }


        }

        private static List<string> GetFootballTeam()
        {
            List<string> footballTeam = new List<string>();
            footballTeam.Add("A");
            footballTeam.Add("B");
            footballTeam.Add("C");
            footballTeam.Add("D");
            footballTeam.Add("E");
            footballTeam.Add("F");

            return footballTeam;
        }

        private static void ScheduleFootballMatch(List<string> gameFixtures, DateTime gameStartDate)
        {
            for (int game = 0; game < gameFixtures.Count; game++)
            {
                Console.WriteLine(gameStartDate.ToString("dddd, dd MMMM yyyy") + " \t Game " + (game + 1) + " --" + gameFixtures[game]);
                gameStartDate = gameStartDate.AddDays(1);

            }
        }
        private static List<string> GenerateMatchFixtures(List<string> footballTeam)
        {
            List<string> fixtureHome = GenerateFixtures(footballTeam);
            List<string> fixtureAway = new List<string>(fixtureHome);

            List<string> gameFixtures = new List<string>();

            gameFixtures.AddRange(fixtureHome);
            gameFixtures.AddRange(fixtureAway);

            return gameFixtures;
        }

        private static List<string> GenerateFixtures(List<string> footballTeam)
        {
            if (footballTeam.Count % 2 != 0)
            {
                throw new Exception("Time size issue");
            }


            List<string> opponentTeam = new List<string>();

            opponentTeam.AddRange(footballTeam); // Copy all the elements.
            opponentTeam.RemoveAt(0); // To exclude the first team.

            int teamsSize = opponentTeam.Count;


            List<string> fixtures = new List<string>();
            for (int team = 0; team < footballTeam.Count - 1; team++)
            {
                fixtures.Add(footballTeam[0] + " v/s " + opponentTeam[team % teamsSize]);

                for (int match = 1; match < footballTeam.Count / 2; match++)
                {
                    int firstTeam = (team + match) % teamsSize;
                    int secondTeam = (team + teamsSize - match) % teamsSize;

                    fixtures.Add(opponentTeam[firstTeam] + " v/s " + opponentTeam[secondTeam]);
                }
            }
            return fixtures;
        }
    }
}
