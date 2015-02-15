using MongoDB.Driver;
using TreeGecko.Library.Archery.Objects;
using TreeGecko.Library.Mongo.DAOs;

namespace TreeGecko.Library.Archery.DAOs
{
    internal class RoundDAO: AbstractMongoDAO<Round>
    {
        public RoundDAO(MongoDatabase _mongoDB) 
            : base(_mongoDB)
        {
            //Competition Shooter
            HasParent = true;
        }

        public override string TableName
        {
            get { return "Rounds"; }
        }
    }
}
