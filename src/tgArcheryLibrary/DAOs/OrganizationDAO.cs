using MongoDB.Driver;
using TreeGecko.Library.Archery.Objects;
using TreeGecko.Library.Mongo.DAOs;

namespace TreeGecko.Library.Archery.DAOs
{
    public class OrganizationDAO : AbstractMongoDAO<Organization>
    {
        public OrganizationDAO(MongoDatabase _mongoDB)
            : base(_mongoDB)
        {
            //Top Level Object
            HasParent = false;
        }

        public override string TableName
        {
            get { return "Organizations"; }
        }
    }
}
