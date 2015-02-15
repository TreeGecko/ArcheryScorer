using MongoDB.Driver;
using TreeGecko.Library.Archery.Objects;
using TreeGecko.Library.Mongo.DAOs;

namespace TreeGecko.Library.Archery.DAOs
{
    internal class ArrowDAO: AbstractMongoDAO<Arrow>
    {
        public ArrowDAO(MongoDatabase _mongoDB) 
            : base(_mongoDB)
        {
            //End
            HasParent = true;
        }

        public override string TableName
        {
            get { return "Arrows"; }
        }
    }
}
