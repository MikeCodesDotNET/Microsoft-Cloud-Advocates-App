using System;
using System.Collections.Generic;

namespace Advocates.Models
{
    public class SearchRequest
    {
        public string SearchTerm { get; set; }

        public List<Advocate> SelectedAdvocates { get; set; }
        public List<string> Source { get; set; }

    }
}
