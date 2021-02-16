


using System;
using System.Globalization;

namespace trendwallapi.Utils
{

    public static class Consts{

        public const string TwitterDateTemplate = "ddd MMM dd HH:mm:ss +ffff yyyy";

    }
    public static class DateHelper
    {
        public static DateTime fromTwitterString(string dateTwitter){

            try
            {
                DateTime dt = DateTime.Parse(dateTwitter);
                
                return dt;
            }
            catch (System.Exception)
            {
                
                throw;
            }
            
        }

        public static DateTime Parse(string from, string ora_from){

            DateTime dt_from = DateTime.ParseExact(from, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            dt_from += DateTime.ParseExact(ora_from, "HHmm", CultureInfo.InvariantCulture).TimeOfDay;
            return dt_from;

        }
    }

}