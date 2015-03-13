using System.Collections.Generic;

namespace TreeGecko.Archery.Server.JsonObjects
{
    public class CompetitionsResult : BaseResult
    {
        public List<CompetitionRow> CompetitionRows { get; set; } 
    }
}