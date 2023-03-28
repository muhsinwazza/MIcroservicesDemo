using MongoDB.Bson.Serialization.Attributes;

namespace CustomerService.Models
{
    public class Customer
    {
        [BsonId,BsonElement("_id"),BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("name"),BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string Name { get; set; }
        [BsonElement("emailId"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string EmailId { get; set; }
        [BsonElement("address"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string Address { get; set; }

    }
}
