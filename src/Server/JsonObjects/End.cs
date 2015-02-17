using System.Collections.Generic;
using TreeGecko.Library.Archery.Managers;

namespace TreeGecko.Archery.Server.JsonObjects
{
    public class End : BaseObject
    {
        public int Sequence { get; set; }
        public int EndScore { get; set; }
        public string Notes { get; set; }
        public List<Arrow> Arrows { get; set; } 

        public End()
        {
            
        }

        public End(Library.Archery.Objects.End _end,
            ArcheryScorerManager _manager)
            : base(_end.Guid)
        {
            Sequence = _end.Sequence;
            EndScore = _end.EndScore;
            Notes = _end.Notes;
            Arrows = new List<Arrow>();

            var arrows = _manager.GetArrows(_end.Guid);
            foreach (var arrow in arrows)
            {
                var jArrow = new Arrow(arrow);
                Arrows.Add(jArrow);
            }
        }

    }
}