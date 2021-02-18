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
    public class LatestTrendsController : ControllerBase
    {

        private readonly ITrendsService _trendsService;

        public LatestTrendsController(ITrendsService trendsService)
        {
            _trendsService = trendsService;
        }

         
        //https://localhost:5001/api/lasttrends

        [HttpGet("{country}")]
        public ActionResult<List<Trend>> Get(string country) 
        {
            
            try
            {
                Trend ultimo =  _trendsService.Latest(country);

                DateTime data_rl = ultimo.Timestamp;

                List<Trend> lasts = _trendsService.GetByTimestamp(data_rl,country.ToUpper());


                return lasts;

            }
            catch (System.Exception e)
            {   
                Console.WriteLine(e.Message);
                throw ; //new ArgumentException( $"Mancano alcuni parametri");
                 
            }
            
             

        }
         





  
         
    }
}
