using System;
using TreeGecko.Library.Common.Objects;

namespace TreeGecko.Library.Archery.Objects
{
    public class Shooter : AbstractTGObject
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }


        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tgs = base.GetTGSerializedObject();

            tgs.Add("FirstName", FirstName);
            tgs.Add("LastName", LastName);
            tgs.Add("BirthDate", BirthDate);

            return tgs;
        }

        public override void LoadFromTGSerializedObject(TGSerializedObject _tgs)
        {
            base.LoadFromTGSerializedObject(_tgs);

            FirstName = _tgs.GetString("FirstName");
            LastName = _tgs.GetString("LastName");
            BirthDate = _tgs.GetDateTime("BirthDate");
        }
    }
}
