using System;
using System.Collections.Generic;

namespace jBot.Lib.Models
{
    public class SearchParams
    {
        public DateTime ModifiedSince { get; set; }
        public List<string> HashTags { get; set; }
        public int MaxItems { get; set; }
        public long SinceId { get; set; }
    }
}
