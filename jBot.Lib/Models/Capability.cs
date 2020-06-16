using System;
namespace jBot.Lib.Models
{
    public class Capability
    {
        public string HashTag { get; set; }
        public string Description { get; set; }
        public string ActionMethod
        {
            get
            {
                return $"{HashTag.Substring(1)}Action";
            }
        }
    }
}
