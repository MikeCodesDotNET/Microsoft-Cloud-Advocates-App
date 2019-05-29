using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Advocates.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Advocates.Services
{
    public class AzureMapService
    {
        public async Task<Address> GetAddress(double latitude, double longitude)
        {
            using (WebClient webClient = new WebClient())
            {
                var format = "json";
                var subscription = Helpers.Constants.AzureMapsApiKey;
                var query = $"{latitude},{longitude}";
                var language = "en-GB";
                var entitys = "Country, Municipality";

                var url = $"https://atlas.microsoft.com/search/address/reverse/{format}?subscription-key={subscription}&api-version=1.0&query={query}&language={language}&entity={entitys}";
                // Download the Web resource and save it into the current filesystem folder.
                var json = await webClient.DownloadStringTaskAsync(url);

                var result = MapResult.FromJson(json);
                var addres = result.Addresses.ToList().First().Address;
                return addres;
            }
        }


        public async Task<Models.TimeZone> GetTimeZone(double latitude, double longitude)
        {
            using (WebClient webClient = new WebClient())
            {
                var format = "json";
                var subscription = "f6qsIPDhV8rSFwzz2LHUfD6gNhW5jcKdsd_JTcexmjg";
                var query = $"{latitude},{longitude}";

                var url = $"https://atlas.microsoft.com/timezone/byCoordinates/{format}?subscription-key={subscription}&api-version=1.0&query={query}";
                // Download the Web resource and save it into the current filesystem folder.
                var json = await webClient.DownloadStringTaskAsync(url);

                var result = TimeZoneResult.FromJson(json);
                var timeZone = result.TimeZones.ToList().First();
                return timeZone;
            }
        }



    }
   

}

