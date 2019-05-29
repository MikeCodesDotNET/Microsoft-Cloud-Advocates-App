using System;
using System.Collections.Generic;
using System.Reflection;

namespace Advocates.Models
{
    public class Advocate : BaseModel
    {

        public string Name { get; set; }
        public string GithubHandle { get; set; }
        public string TwitterHandle { get; set; }
        public string AvatarUrl { get; set; }
        public string Bio { get; set; }
        public List<string> Skills { get; set; }

        //Used for location & timezone lookup
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public List<IgniteStop> IgniteStops { get; set; }

        public Advocate()
        {
            Skills = new List<string>();
            IgniteStops = new List<IgniteStop>();
        }

        public override string ClassType { get; set; }//=> MethodBase.GetCurrentMethod().DeclaringType.Name;

        public class IgniteStop
        {
            public string Name { get; set; }
            public string Image { get; set; }
        }
    }
}
