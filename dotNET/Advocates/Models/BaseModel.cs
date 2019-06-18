using System;
namespace Advocates.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }


        // use virtual keyword
        public virtual string ClassType { get; set; }
    }
}

