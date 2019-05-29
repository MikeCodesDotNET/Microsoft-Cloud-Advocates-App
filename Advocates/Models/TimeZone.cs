using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Advocates.Models
{
    public class TimeZone
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Aliases")]
        public string[] Aliases { get; set; }



        [JsonProperty("Names")]
        public Names Names { get; set; }

        [JsonProperty("ReferenceTime")]
        public ReferenceTime ReferenceTime { get; set; }

      
    }

   

    public class Names
    {
        [JsonProperty("ISO6391LanguageCode")]
        public string Iso6391LanguageCode { get; set; }

        [JsonProperty("Generic")]
        public string Generic { get; set; }

        [JsonProperty("Standard")]
        public string Standard { get; set; }

        [JsonProperty("Daylight")]
        public string Daylight { get; set; }
    }

    public class ReferenceTime
    {
        [JsonProperty("Tag")]
        public string Tag { get; set; }

        [JsonProperty("StandardOffset")]
        public string StandardOffset { get; set; }


        [JsonProperty("DaylightSavings")]
        public string DaylightSavings { get; set; }


        [JsonProperty("SunRise")]
        public DateTimeOffset SunRise { get; set; }

        [JsonProperty("SunSet")]
        public DateTimeOffset SunSet { get; set; }
    }

    public partial class RepresentativePoint
    {
        [JsonProperty("Latitude")]
        public double Latitude { get; set; }

        [JsonProperty("Longitude")]
        public double Longitude { get; set; }
    }


    public partial class TimeZoneResult
    {
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("timeZones")]
        public TimeZone[] TimeZones { get; set; }

        public static TimeZoneResult FromJson(string json) => JsonConvert.DeserializeObject<TimeZoneResult>(json, settings);


        static JsonSerializerSettings settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateParseHandling = DateParseHandling.DateTimeOffset, 
            Converters =
            {
                //2019-05-26T05:20:30.922+00:00

                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

}
