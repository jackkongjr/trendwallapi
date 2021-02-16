

using System.Collections.Generic;
using trendwallapi.Models;
using System;

namespace trendwallapi.Interfaces
{

    public interface ITrendsService
    {

        List<Trend> Get();

        Trend Get(string id);
        
        List<Trend> Query(DateTime from, DateTime to, string country);

        Trend Create(Trend trend);

        void Update(string id, Trend trendIn);

        void Remove( Trend trendIn);

        void Remove(string id);



    }


}