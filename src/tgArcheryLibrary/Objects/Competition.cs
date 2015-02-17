using System;
using TreeGecko.Library.Common.Objects;

namespace TreeGecko.Library.Archery.Objects
{
    public class Competition : AbstractTGObject
    {
        public string Name { get; set; }
        public string Organization { get; set; }
        public DateTime Date { get; set; }
        public int Rounds { get; set; }
        public int Ends { get; set; }
        public int ArrowsPerEnd { get; set; }
        public int MaxPointsPerArrow { get; set; }
        public bool TrackX { get; set; }

        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tgs = base.GetTGSerializedObject();

            tgs.Add("Name", Name);
            tgs.Add("Organization", Organization);
            tgs.Add("DateTime", Date);
            tgs.Add("Rounds", Rounds);
            tgs.Add("Ends", Ends);
            tgs.Add("ArrowsPerEnd", ArrowsPerEnd);
            tgs.Add("MaxPointsPerArrow", MaxPointsPerArrow);
            tgs.Add("TrackX", TrackX);

            return tgs;
        }

        public override void LoadFromTGSerializedObject(TGSerializedObject _tgs)
        {
            base.LoadFromTGSerializedObject(_tgs);

            Name = _tgs.GetString("Name");
            Organization = _tgs.GetString("Organization");
            Date = _tgs.GetDateTime("DateTime");
            Rounds = _tgs.GetInt32("Rounds");
            Ends = _tgs.GetInt32("Ends");
            ArrowsPerEnd = _tgs.GetInt32("ArrowsPerEnd");
            MaxPointsPerArrow = _tgs.GetInt32("MaxPointsPerArrow");
            TrackX = _tgs.GetBoolean("TrackX");
        }
    }
}
