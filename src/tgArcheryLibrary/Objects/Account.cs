using System;
using TreeGecko.Library.Common.Objects;

namespace TreeGecko.Library.Archery.Objects
{
    public class Account : AbstractTGObject
    {
        public Guid PrimaryUserGuid { get; set; }

        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tgs = base.GetTGSerializedObject();

            tgs.Add("PrimaryUserGuid", PrimaryUserGuid);

            return tgs;
        }

        public override void LoadFromTGSerializedObject(TGSerializedObject _tgs)
        {
            base.LoadFromTGSerializedObject(_tgs);

            PrimaryUserGuid = _tgs.GetGuid("PrimaryUserGuid");
        }
    }
}
