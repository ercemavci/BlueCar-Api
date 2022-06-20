using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueCar_Api
{
    public class MongoDbSettings
    {
        public string ConnectionString;
        public string Database;
        public string DbCollection;

        #region Const Values

        public const string ConnectionStringValue = nameof(ConnectionString);
        public const string DatabaseValue = nameof(Database);
        public const string DbCollectionValue = nameof(DbCollection);

        #endregion
    }
}
