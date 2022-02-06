using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Octopus.Core.Common.DynamicObject.Models
{
    public class DynamicEntityWithProperties
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
        public string EntityName { get; set; }
        public IList<DynamicProperty> Properties { get; set; }
    }
}
