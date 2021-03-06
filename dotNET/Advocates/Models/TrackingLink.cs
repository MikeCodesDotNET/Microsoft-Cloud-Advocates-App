﻿using System;
namespace Advocates.Models
{
    public class TrackingLink : BaseModel
    {
        public string Url { get; set; }
        public string Event { get; set; }
        public string Channel { get; set; }
        public string Alias { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ShareableLink { get; set; }
    }
}
