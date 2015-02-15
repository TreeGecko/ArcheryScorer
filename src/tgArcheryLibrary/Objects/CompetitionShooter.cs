using System;
using TreeGecko.Library.Common.Objects;

namespace TreeGecko.Library.Archery.Objects
{
    public class CompetitionShooter : AbstractTGObject
    {
        public Guid ShooterGuid { get; set; }
        public string Target { get; set; }
        public string Notes { get; set; }


    }
}
