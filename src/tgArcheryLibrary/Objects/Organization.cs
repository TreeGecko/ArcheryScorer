using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using TreeGecko.Library.Common.Objects;

namespace TreeGecko.Library.Archery.Objects
{
    public class Organization : AbstractTGObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Guid> CompetitionFormatGuids { get; set; }

        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tgs = base.GetTGSerializedObject();

            tgs.Add("Name", Name);
            tgs.Add("Description", Description);
            tgs.Add("CompetitionFormatGuids", CompetitionFormatGuids);

            return tgs;
        }

        public override void LoadFromTGSerializedObject(TGSerializedObject _tgs)
        {
            base.LoadFromTGSerializedObject(_tgs);

            Name = _tgs.GetString("Name");
            Description = _tgs.GetString("Description");
            CompetitionFormatGuids = _tgs.GetListOfGuids("CompetitionFormatGuids");
        }
    }
}
