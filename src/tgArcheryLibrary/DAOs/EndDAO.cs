using MongoDB.Driver;
using TreeGecko.Library.Archery.Objects;
using TreeGecko.Library.Mongo.DAOs;

namespace TreeGecko.Library.Archery.DAOs
{
    internal class EndDAO: AbstractMongoDAO<End>
    {
        public EndDAO(MongoDatabase _mongoDB) 
            : base(_mongoDB)
        {
            //Round
            HasParent = true;
        }

        public override string TableName
        {
            get { return "Ends"; }
        }
    }
}
