using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace trendwallapi.Models
{
    public class Trend
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }


        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("query")]
        public string Query { get; set; }

        [BsonElement("url")]
        public string Url { get; set; }


        private DateTime _timestamp;
        [BsonElement("timestamp")]               
        public DateTime Timestamp {
            get{
                return _timestamp.ToLocalTime();
            }
            set{ _timestamp = value;}
        }

        [BsonElement("count")]
        public int Count { get; set; }


        [BsonElement("country")]
        public string Country { get; set; }


    }





}
