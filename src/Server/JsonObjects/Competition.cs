using System;
using System.Collections.Generic;
using System.Text;
using TreeGecko.Library.Archery.Managers;

namespace TreeGecko.Archery.Server.JsonObjects
{
    public class Competition : BaseObject
    {
        public string Name { get; set; }
        public string Organization { get; set; }
        public string Date { get; set; }
        public int Rounds { get; set; }
        public int Ends { get; set; }
        public int ArrowsPerEnd { get; set; }
        public int MaxPointsPerArrow { get; set; }
        public bool TrackX { get; set; }
        public bool IsComplete { get; set; }
        public List<CompetitionShooter> CompetitionShooters { get; set; }

        public Competition()
        {

        }

        public Competition(Library.Archery.Objects.Competition _competition,
            ArcheryScorerManager _manager)
            : base(_competition.Guid)
        {
            Name = _competition.Name;
            Organization = _competition.Organization;
            Date = _competition.Date.ToString("d");
            Rounds = _competition.Rounds;
            Ends = _competition.Ends;
            ArrowsPerEnd = _competition.ArrowsPerEnd;
            MaxPointsPerArrow = _competition.MaxPointsPerArrow;
            TrackX = _competition.TrackX;
            CompetitionShooters = new List<CompetitionShooter>();

            var competitionShooters = _manager.GetCompetitionShooters(_competition.Guid);
            foreach (var competitionShooter in competitionShooters)
            {
                var jCompetitionShooter = new CompetitionShooter(competitionShooter, _manager);
                CompetitionShooters.Add(jCompetitionShooter);
            }
        }

    }
}