using System.Collections.Generic;
using TreeGecko.Library.Archery.Managers;

namespace TreeGecko.Archery.Server.JsonObjects
{
    public class Round : BaseObject
    {
        public int Sequence { get; set; }
        public int RoundScore { get; set; }
        public string Notes { get; set; }
        public List<End> Ends { get; set; } 

        public Round()
        {
            
        }

        public Round(Library.Archery.Objects.Round _round, 
            ArcheryScorerManager _manager)
            : base(_round.Guid)
        {
            Sequence = _round.Sequence;
            RoundScore = _round.RoundScore;
            Notes = _round.Notes;
            Ends = new List<End>();

            var ends = _manager.GetEnds(_round.Guid);
            foreach (var end in ends)
            {
                var jEnd = new End(end, _manager);
                Ends.Add(jEnd);
            }
        }

    }
}