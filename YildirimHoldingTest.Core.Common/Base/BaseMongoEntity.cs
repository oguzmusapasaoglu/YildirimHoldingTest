using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace YildirimHoldingTest.Core.Common.Base
{
    public class BaseMongoEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public static ObjectId GenerateId => ObjectId.GenerateNewId();
    }
}
