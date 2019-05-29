using System;
using Advocates.Helpers;
using Newtonsoft.Json;

namespace Advocates.Models
{
    public class AddressElement
    {
        [JsonProperty("address")]
        public Address Address { get; set; }

        [JsonProperty("position")]
        public string Position { get; set; }
    }

    public class Address
    {
        [JsonProperty("buildingNumber")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long BuildingNumber { get; set; }

        [JsonProperty("streetNumber")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long StreetNumber { get; set; }

        [JsonProperty("routeNumbers")]
        public object[] RouteNumbers { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("streetName")]
        public string StreetName { get; set; }

        [JsonProperty("streetNameAndNumber")]
        public string StreetNameAndNumber { get; set; }

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("countrySubdivision")]
        public string CountrySubdivision { get; set; }

        [JsonProperty("countrySubdivisionName")]
        public string CountrySubdivisionName { get; set; }

        [JsonProperty("municipality")]
        public string Municipality { get; set; }

        [JsonProperty("postalCode")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long PostalCode { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("countryCodeISO3")]
        public string CountryCodeIso3 { get; set; }

        [JsonProperty("freeformAddress")]
        public string FreeformAddress { get; set; }

        [JsonProperty("extendedPostalCode")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ExtendedPostalCode { get; set; }

        public string Printable => $"{Municipality}, {Country}";

       
    }
}
