using System;
using TreeGecko.Library.Common.Objects;

namespace TreeGecko.Library.Archery.Objects
{
    public class CompetitionShooter : AbstractTGObject
    {
        public Guid ShooterGuid { get; set; }
        public string Target { get; set; }
        public string Notes { get; set; }

        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tgs = base.GetTGSerializedObject();

            tgs.Add("ShooterGuid", ShooterGuid);
            tgs.Add("Target", Target);
            tgs.Add("Notes", Notes);

            return tgs;
        }

        public override void LoadFromTGSerializedObject(TGSerializedObject _tgs)
        {
            base.LoadFromTGSerializedObject(_tgs);
            
            ShooterGuid = _tgs.GetGuid("ShooterGuid");
            Target = _tgs.GetString("Target");
            Notes = _tgs.GetString("Notes");
        }
    }
}
