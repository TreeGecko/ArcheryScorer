using System;
using System.Collections.Generic;
using MongoDB.Driver;
using TreeGecko.Library.Archery.Objects;
using TreeGecko.Library.Mongo.DAOs;

namespace TreeGecko.Library.Archery.DAOs
{
    internal class ShooterDAO : AbstractMongoDAO<Shooter>
    {
        public ShooterDAO(MongoDatabase _mongoDB) 
            : base(_mongoDB)
        {
            //Account
            HasParent = true;
        }

        public override string TableName
        {
            get { return "Shooters"; }
        }

        public List<Shooter> GetShooters(Guid _accountGuid)
        {
            return GetChildrenOf(_accountGuid);
        }
    }
}
