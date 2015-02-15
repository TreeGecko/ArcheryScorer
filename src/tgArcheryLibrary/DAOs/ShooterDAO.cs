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
            //Top Level Object
            HasParent = false;
        }

        public override string TableName
        {
            get { return "Shooters"; }
        }
    }
}
