using System;
using System.Collections.Generic;
using MongoDB.Driver;
using TreeGecko.Library.Archery.Objects;
using TreeGecko.Library.Mongo.DAOs;

namespace TreeGecko.Library.Archery.DAOs
{
    internal class CompetitionDAO : AbstractMongoDAO<Competition>
    {
        public CompetitionDAO(MongoDatabase _mongoDB) 
            : base(_mongoDB)
        {
            //Account
            HasParent = true;
        }

        public override string TableName
        {
            get { return "Competitions"; }
        }

        public List<Competition> GetCompetitions(Guid _accountGuid)
        {
            return GetChildrenOf(_accountGuid);
        }
    }
}
