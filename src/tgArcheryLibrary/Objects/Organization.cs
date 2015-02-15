using System;
using System.Collections.Generic;
using TreeGecko.Library.Common.Objects;

namespace TreeGecko.Library.Archery.Objects
{
    public class Organization : AbstractTGObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Guid> CompetitionFormatGuids { get; set; } 
    }
}
