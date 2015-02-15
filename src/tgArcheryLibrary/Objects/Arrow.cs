using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TreeGecko.Library.Common.Objects;

namespace TreeGecko.Library.Archery.Objects
{
    public class Arrow : AbstractTGObject
    {
        public int Score { get; set; }
        public bool IsX { get; set; }

        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tgs = base.GetTGSerializedObject();

            tgs.Add("Score", Score);
            tgs.Add("IsX", IsX);

            return tgs;
        }

        public override void LoadFromTGSerializedObject(TGSerializedObject _tgs)
        {
            base.LoadFromTGSerializedObject(_tgs);

            Score = _tgs.GetInt32("Score");
            IsX = _tgs.GetBoolean("IsX");
        }
    }
}
