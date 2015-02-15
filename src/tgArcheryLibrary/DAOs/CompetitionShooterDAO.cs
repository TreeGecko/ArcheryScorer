using MongoDB.Driver;
using TreeGecko.Library.Archery.Objects;
using TreeGecko.Library.Mongo.DAOs;

namespace TreeGecko.Library.Archery.DAOs
{
    internal class CompetitionShooterDAO: AbstractMongoDAO<CompetitionShooter>
    {
        public CompetitionShooterDAO(MongoDatabase _mongoDB) 
            : base(_mongoDB)
        {
            //Competition
            HasParent = true;
        }

        public override string TableName
        {
            get { return "CompetitionShooters"; }
        }
    }
}
