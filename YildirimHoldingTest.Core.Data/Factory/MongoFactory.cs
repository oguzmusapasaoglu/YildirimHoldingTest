using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using YildirimHoldingTest.Core.Common.Base;
using YildirimHoldingTest.Core.Data.Helper;
using YildirimHoldingTest.Core.Data.Interfaces;

namespace YildirimHoldingTest.Core.Data.Factory
{
    public class MongoFactory : MongoDefinitions, IMongoFactory
    {
        public List<T> GetAllByField<T>(string collectionName, string fieldName, object fieldValue) where T : BaseMongoEntity
        {
            try
            {
                var collection = GetCollection<T>(collectionName);
                var filter = Builders<T>.Filter.Eq(fieldName, fieldValue);
                return collection.Find(filter).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<T> GetAllData<T>(string collectionName) where T : BaseMongoEntity
        {
            throw new NotImplementedException();
        }

        public IEnumerable<int> InsertMany<T>(string collectionName, List<T> data) where T : BaseMongoEntity
        {
            IEnumerable<int> result = new List<int>();
            var database = GetDatabase("");
            var members = database.GetCollection<T>(collectionName);
            database.CreateCollection(collectionName);
            try
            {
                members.InsertMany(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing to MongoDB: " + ex.Message);
            }
            return result;
        }

        public bool InsertOne<T>(string collectionName, T data) where T : BaseMongoEntity
        {
            try
            {
                var collection = GetCollection<T>(collectionName);
                collection.InsertOne(data);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing to MongoDB: " + ex.Message);
                return false;
            }
        }

        public bool Update<T>(string collectionName, T member, string fieldName, object fieldData) where T : BaseMongoEntity
        {
            throw new NotImplementedException();
        }

        public bool UpdateOne<T>(string collectionName, string filterColumn, object filterValue, BsonDocument bsonDocument) where T : BaseMongoEntity
        {
            try
            {
                var collection = GetCollection<T>(collectionName);
                var filter = Builders<T>.Filter.Eq(filterColumn, filterValue);
                var update = bsonDocument;
                var result = collection.UpdateOne(filter, update);
                return result.ModifiedCount != 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T GetSingleByField<T>(string collectionName, string fieldName, object fieldValue) where T : BaseMongoEntity
        {
            var filter = Builders<T>.Filter.Eq(fieldName, fieldValue);
            var collection = GetCollection<T>(collectionName);
            var result = collection.Find(filter).FirstOrDefault();
            return result;
        }

        public bool DeleteOne<T>(string collectionName, string fieldName, object fieldValue) where T : BaseMongoEntity
        {
            try
            {
                var collection = GetCollection<T>(collectionName);
                var filter = Builders<T>.Filter.Eq(fieldName, fieldValue);
                var result = collection.DeleteMany(filter);
                return result.DeletedCount != 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteMany<T>(string collectionName, string fieldName, object fieldValue) where T : BaseMongoEntity
        {
            try
            {
                var collection = GetCollection<T>(collectionName);
                var filter = Builders<T>.Filter.Eq(fieldName, fieldValue);
                var result = collection.DeleteMany(filter);
                return result.DeletedCount != 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private IMongoCollection<T> GetCollection<T>(string collectionName) where T : BaseMongoEntity
        {
            var database = GetDatabase("");
            var collection = database.GetCollection<T>(collectionName);
            return collection;
        }
    }
}
