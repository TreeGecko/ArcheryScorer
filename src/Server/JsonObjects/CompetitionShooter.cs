using System.Collections.Generic;
using TreeGecko.Library.Archery.Managers;

namespace TreeGecko.Archery.Server.JsonObjects
{
    public class CompetitionShooter : BaseObject
    {
        public string ShooterFullName { get; set; }
        public string ShooterGuid { get; set; }
        public string Target { get; set; }
        public string Notes { get; set; }
        public List<Round> Rounds {get; set; } 

        public CompetitionShooter()
        {
            
        }

        public CompetitionShooter(Library.Archery.Objects.CompetitionShooter _competitionShooter, 
            ArcheryScorerManager _manager )
            : base(_competitionShooter.Guid)
        {
            ShooterGuid = _competitionShooter.ShooterGuid.ToString();

            var shooter = _manager.GetShooter(_competitionShooter.ShooterGuid);
            if (shooter != null)
            {
                ShooterFullName = shooter.FullName;
            }
            
            Target = _competitionShooter.Target;
            Notes = _competitionShooter.Notes;
            Rounds = new List<Round>();

            var rounds = _manager.GetRounds(_competitionShooter.Guid);
            foreach (var round in rounds)
            {
                var jRound = new Round(round, _manager);
                Rounds.Add(jRound);
            }
        }

    }
}