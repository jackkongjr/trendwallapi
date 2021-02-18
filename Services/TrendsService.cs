using trendwallapi.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using trendwallapi.Interfaces;
using System;
using MongoDB.Driver.Linq;

namespace trendwallapi.Services
{
    public class TrendsService:ITrendsService
    {
        private readonly IMongoCollection<Trend> _trends;

        public TrendsService(ITrendsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _trends = database.GetCollection<Trend>(settings.TrendsCollectionName);
        }

        public List<Trend> Get() =>
            _trends.Find(trend => true).ToList();


        public Trend Latest(string country) {
             
            Trend allDocs = _trends.AsQueryable().OrderByDescending(c => c.Id).First();;

            return allDocs;
        }


        public List<Trend> Query(DateTime from, DateTime to,string country) {
            
            var filter = Builders<Trend>.Filter.Eq("country", country);
                
            List<Trend> res = _trends.Find<Trend>(trend => trend.Country.Equals(country) &&  
            trend.Timestamp>=from && trend.Timestamp <= to )
           // .Project<Trend>(Builders<Trend>.Projection.Exclude(t => t.Id))
            .ToList();

            return res;
        }


        public List<Trend> GetByTimestamp(DateTime tstamp,string country) =>
            _trends.Find<Trend>(trend => trend.Timestamp == tstamp && trend.Country.Equals(country)).ToList();


        public Trend Get(string name) =>
            _trends.Find<Trend>(trend => trend.Name == name).FirstOrDefault();

        public Trend Create(Trend trend)
        {
            _trends.InsertOne(trend);
            return trend;
        }

        public void Update(string id, Trend trendIn) =>
            _trends.ReplaceOne(trend => trend.Id == id, trendIn);

        public void Remove( Trend trendIn) =>
            _trends.DeleteOne(trend => trend.Id == trendIn.Id);

        public void Remove(string id) => 
            _trends.DeleteOne(trend => trend.Id == id);
    }
}
