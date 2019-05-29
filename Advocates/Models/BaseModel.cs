using System;
namespace Advocates.Models
{
    public abstract class BaseModel
    {
        public Guid Id { get; set; } = Guid.NewGuid(); //generates random id


        // use virtual keyword
        public virtual string ClassType { get; set; }
    }
}

