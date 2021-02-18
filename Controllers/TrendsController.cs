using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using trendwallapi.Interfaces;
using trendwallapi.Models;
using trendwallapi.Services;

using System.Text.Json;
using System.Text.Json.Serialization;



namespace trendwallapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TrendsController : ControllerBase
    {

        private readonly ITrendsService _trendsService;

        public TrendsController(ITrendsService trendsService)
        {
            _trendsService = trendsService;
        }

         
        //https://localhost:5001/api/trends/14-02-2021/14-02-2021/0000/1600/it

        [HttpGet("{from}/{to}/{ora_from}/{ora_to}/{country}")]
        public ActionResult<Dictionary<string, object>> Get(string from, string to, string ora_from, string ora_to, string country) 
        {
            
            try
            {
                //tendenze attuali
                // Trend ultimo =  _trendsService.Latest(country);
                // DateTime data_rl = ultimo.Timestamp;
                // List<Trend> lasts = _trendsService.GetByTimestamp(data_rl,country.ToUpper());


                Dictionary<string,object> result = new Dictionary<string, object>();


                DateTime dt_from = Utils.DateHelper.Parse(from,ora_from);
                DateTime dt_to = Utils.DateHelper.Parse(to,ora_to);
                country = country.ToUpper().Substring(0,2);

                List<Trend> query =  _trendsService.Query(dt_from,dt_to,country);

                List<IGrouping<string, Trend>> list = query.GroupBy(x => x.Timestamp.ToString()).ToList();

                
                // prelevo le categorie del grafico
                var categorie = (from item in query 
                                group item by item.Timestamp
                                into categorieClass
                                select categorieClass).ToDictionary(gdc => gdc.Key.ToString("dd-MM-yyyy HH:mm")).Keys;

                
                var per_hashtag = (from item in query 
                                group item by item.Name
                                into categorieClass
                                select categorieClass).ToDictionary(gdc => gdc.Key,gdc => gdc.ToList());

                var per_hash = query.GroupBy(x=>x.Name,StringComparer.InvariantCultureIgnoreCase)
                .ToDictionary(gdc => gdc.Key,gdc => gdc.ToList());
                
                List<Dictionary<string,object>> series = new List<Dictionary<string, object>>();
                foreach (var item in per_hash)
                { 
                    //if(!item.Key.StartsWith("#"))continue;
                    Dictionary<string,object> serie = new Dictionary<string, object>();
                    List<int> data = new List<int>();    
                    foreach (var cat in categorie)
                    {
                        int count = 0;
                        int max = item.Value.Max( r => r.Count);
                        foreach (var itemcat in item.Value)
                        {
                            string datestr = itemcat.Timestamp.ToString("dd-MM-yyyy HH:mm");
                            if( datestr.Equals(cat)){
                                if(itemcat.Count==max){
                                    count = itemcat.Count;
                                }
                            }
                        }   
                        data.Add(count);
                    }
                    serie.Add("name",item.Key);
                    serie.Add("data",data);
                    Dictionary<string, object> marker = new Dictionary<string, object>();
                    marker.Add("enabled",false);
                    marker.Add("symbol","square");
                    serie.Add("marker",marker);
                    series.Add(serie);
                }
                
                result.Add("categories",categorie);
                result.Add("series",series);

                //result.Add("current",lasts);
                return result;
               
            }
            catch (System.Exception e)
            {   
                Console.WriteLine(e.Message);
                throw ; //new ArgumentException( $"Mancano alcuni parametri");
                 
            }
            
             

        }
         





  
         
    }
}
