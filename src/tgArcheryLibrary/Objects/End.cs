using TreeGecko.Library.Common.Objects;

namespace TreeGecko.Library.Archery.Objects
{
    public class End : AbstractTGObject
    {
        public int Sequence { get; set; }
        public int EndScore { get; set; }
        public string Notes { get; set; }
        
        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tgs = base.GetTGSerializedObject();

            tgs.Add("Sequence", Sequence);
            tgs.Add("EndScore", EndScore);
            tgs.Add("Notes", Notes);

            return tgs;
        }

        public override void LoadFromTGSerializedObject(TGSerializedObject _tgs)
        {
            base.LoadFromTGSerializedObject(_tgs);

            Sequence = _tgs.GetInt32("Sequence");
            EndScore = _tgs.GetInt32("EndScore");
            Notes = _tgs.GetString("Notes");
        }
    }
}
