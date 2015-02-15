using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TreeGecko.Library.Common.Objects;

namespace TreeGecko.Library.Archery.Objects
{
    public class Round : AbstractTGObject
    {
        public int Sequence { get; set; }
        public Guid Shooter { get; set; }

        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tgs = base.GetTGSerializedObject();

            tgs.Add("Sequence", Sequence);
            tgs.Add("Shooter", Shooter);

            return tgs;
        }

        public override void LoadFromTGSerializedObject(TGSerializedObject _tgs)
        {
            base.LoadFromTGSerializedObject(_tgs);

            Sequence = _tgs.GetInt32("Sequence");
            Shooter = _tgs.GetGuid("Shooter");
        }
    }
}
