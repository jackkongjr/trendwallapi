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
    public class MapDataController : ControllerBase
    {

        private readonly ITrendsService _trendsService;

        public MapDataController(ITrendsService trendsService)
        {
            _trendsService = trendsService;
        }


     [HttpGet]
        public ActionResult<Dictionary<string, List<Trend>>> Get(string from, string to, string ora_from, string ora_to, string country) 
        {
            
            try
            {
                //tendenze attuali
                Trend ultimo =  _trendsService.Latest("ALL");
                DateTime data_rl = ultimo.Timestamp;
                List<Trend> lasts = _trendsService.GetByTimestampNotGrouped(data_rl,"ALL");
                //.Where(x=>x.Name.StartsWith("#")).ToList();

                var group = lasts.GroupBy(o =>  o.Country.ToLower() ,StringComparer.InvariantCultureIgnoreCase)
                .ToList().ToDictionary(gdc => gdc.Key,gdc => gdc.ToList());
              
                var per_hashtag = (from item in lasts 
                                group item by item.Country
                                into categorieClass
                                select categorieClass).ToDictionary(gdc => gdc.Key,gdc => gdc.ToList());
                
                return per_hashtag;
               
            }
            catch (System.Exception e)
            {   
                Console.WriteLine(e.Message);
                throw ; //new ArgumentException( $"Mancano alcuni parametri");
                 
            }
            
             

        }







    }



}