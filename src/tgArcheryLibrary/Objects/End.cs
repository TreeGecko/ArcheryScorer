using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TreeGecko.Library.Common.Objects;

namespace TreeGecko.Library.Archery.Objects
{
    public class End : AbstractTGObject
    {
        public int Sequence { get; set; }
        
        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tgs = base.GetTGSerializedObject();

            tgs.Add("Sequence", Sequence);

            return tgs;
        }

        public override void LoadFromTGSerializedObject(TGSerializedObject _tgs)
        {
            base.LoadFromTGSerializedObject(_tgs);

            Sequence = _tgs.GetInt32("Sequence");
        }
    }
}
