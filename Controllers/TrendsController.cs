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
        public ActionResult<List<KeyValuePair<string, List<Trend>>>> Get(string from, string to, string ora_from, string ora_to, string country) 
        {
            
            try
            {
                DateTime dt_from = Utils.DateHelper.Parse(from,ora_from);
                DateTime dt_to = Utils.DateHelper.Parse(to,ora_to);
                country = country.ToUpper().Substring(0,2);

                List<Trend> query =  _trendsService.Query(dt_from,dt_to,country);

                List<IGrouping<string, Trend>> list = query.GroupBy(x => x.Timestamp.ToString())
                
                .ToList();

                
                // prelevo le categorie del grafico
                var categorie = (from item in query 
                                group item by item.Timestamp
                                into categorieClass
                                select categorieClass).ToDictionary(gdc => gdc.Key.ToString("dd-MM-yyyy HH:mm")).Keys;

                
                var per_hashtag = (from item in query 
                                group item by item.Name
                                into categorieClass
                                select categorieClass).ToDictionary(gdc => gdc.Key,gdc => gdc.ToList());

                foreach (var item in query)
                {
                    
                }
                var groupedDemoClasses = (from item in query
                          group item by item.Timestamp
                          into groupedDemoClass
                          select groupedDemoClass).ToDictionary(gdc => gdc.Key.ToString("dd-MM-yyyy HH:mm"), gdc => gdc.ToList());

                return groupedDemoClasses.ToList();
               
            }
            catch (System.Exception e)
            {   
                Console.WriteLine(e.Message);
                throw ; //new ArgumentException( $"Mancano alcuni parametri");
                 
            }
            
             

        }
         





  
         
    }
}
