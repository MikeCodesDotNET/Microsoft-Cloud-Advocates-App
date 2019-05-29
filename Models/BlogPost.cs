using System;
using System.Reflection;
using Newtonsoft.Json;

namespace Advocates.Models
{
    public class BlogPost : BaseModel
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("isFamilyFriendly")]
        public bool IsFamilyFriendly { get; set; }

        [JsonProperty("primaryImage")]
        public PrimaryImage PrimaryImage { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }


    }

    public partial class PrimaryImage
    {
        [JsonProperty("contentUrl")]
        public Uri ContentUrl { get; set; }
    }
}
