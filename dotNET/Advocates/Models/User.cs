using System;
namespace Advocates.Models
{
    public class User : BaseModel
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Alias { get; set; } 
    }
}
