using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Advocates.Models
{
    public class MapResult
    {
        [JsonProperty("summary")]
        public Summary Summary { get; set; }

        public static MapResult FromJson(string json) => JsonConvert.DeserializeObject<MapResult>(json, settings);

        [JsonProperty("addresses")]
        public AddressElement[] Addresses { get; set; }

        static JsonSerializerSettings settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    public class Summary
    {
        [JsonProperty("queryTime")]
        public long QueryTime { get; set; }

        [JsonProperty("numResults")]
        public long NumResults { get; set; }
    }


}
