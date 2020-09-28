using MongoDB.Driver;
using YildirimHoldingTest.Core.Common.Base;
using YildirimHoldingTest.Core.Common.ConfigHelper;
using YildirimHoldingTest.Core.Common.Helper;

namespace YildirimHoldingTest.Core.Data.Helper
{
    public class MongoDefinitions
    {
        public MongoDbConfigModel mongoDbConfig => AppConfigHelper.GetMongoInfo();
        public MongoClient GetClient(string connectionString = "")
        {
            return new MongoClient((connectionString.IsNullOrEmpty())
                ? mongoDbConfig.ConnectionString
                : connectionString);
        }
        public IMongoDatabase GetDatabase(string dbName, MongoClient client = null)
        {
            try
            {
                if (client == null)
                    client = GetClient();
                return client.GetDatabase(dbName);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public IMongoCollection<T> GetCollection<T>(IMongoDatabase database, string collectionName) where T : BaseMongoEntity
        {
            var collection = database.GetCollection<T>(collectionName);
            return collection;
        }
    }
}
