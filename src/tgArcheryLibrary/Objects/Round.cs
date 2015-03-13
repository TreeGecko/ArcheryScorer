using System.Collections.Generic;
using TreeGecko.Library.Common.Objects;

namespace TreeGecko.Library.Archery.Objects
{
    public class Round : AbstractTGObject
    {
        public int Sequence { get; set; }
        public int RoundScore { get; set; }
        public string Notes { get; set; }
        
        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tgs = base.GetTGSerializedObject();

            tgs.Add("Sequence", Sequence);
            tgs.Add("RoundScore", RoundScore);
            tgs.Add("Notes", Notes);
            
            return tgs;
        }

        public override void LoadFromTGSerializedObject(TGSerializedObject _tgs)
        {
            base.LoadFromTGSerializedObject(_tgs);

            Sequence = _tgs.GetInt32("Sequence");
            RoundScore = _tgs.GetInt32("RoundScore");
            Notes = _tgs.GetString("Notes");
        }
    }
}
