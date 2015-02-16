using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using TreeGecko.Library.Archery.Objects;
using TreeGecko.Library.Mongo.DAOs;

namespace TreeGecko.Library.Archery.DAOs
{
    internal class AccountDAO : AbstractMongoDAO<Account>
    {
        public AccountDAO(MongoDatabase _mongoDB) 
            : base(_mongoDB)
        {
            //Top Level Object
            HasParent = false;
        }

        public override string TableName
        {
            get { return "Accouts"; }
        }

        public override void BuildTable()
        {
            base.BuildTable();

            BuildUniqueIndex("PrimaryUserGuid", "USER");
        }

        public Account GetAccountByUser(Guid _userGuid)
        {
            return GetOneItem<Account>("PrimaryUserGuid", _userGuid.ToString());
        }
    }
}
