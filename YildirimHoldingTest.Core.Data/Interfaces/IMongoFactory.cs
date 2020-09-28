using MongoDB.Bson;
using System.Collections.Generic;
using YildirimHoldingTest.Core.Common.Base;

namespace YildirimHoldingTest.Core.Data.Interfaces
{
    public interface IMongoFactory
    {
        List<T> GetAllByField<T>(string collectionName, string fieldName, object fieldValue) where T : BaseMongoEntity;
        List<T> GetAllData<T>(string collectionName) where T : BaseMongoEntity;
        IEnumerable<int> InsertMany<T>(string collectionName, List<T> data) where T : BaseMongoEntity;
        bool InsertOne<T>(string collectionName, T data) where T : BaseMongoEntity;
        bool Update<T>(string collectionName, T member, string fieldName, object fieldData) where T : BaseMongoEntity;
        bool UpdateOne<T>(string collectionName, string filterColumn, object filterValue, BsonDocument bsonDocument) where T : BaseMongoEntity;
        T GetSingleByField<T>(string collectionName, string fieldName, object fieldValue) where T : BaseMongoEntity;
        bool DeleteOne<T>(string collectionName, string fieldName, object fieldValue) where T : BaseMongoEntity;
        bool DeleteMany<T>(string collectionName, string fieldName, object fieldValue) where T : BaseMongoEntity;
    }
}
