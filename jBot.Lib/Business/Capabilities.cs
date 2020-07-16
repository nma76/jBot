using System;
using System.Collections.Generic;
using jBot.Lib.Models;

namespace jBot.Lib.Business
{
    public static class Capabilities
    {
        public static List<Capability> GetAll()
        {
            return new List<Capability>()
            {
                //new Capability()
                //{
                //    HashTag = "#help",
                //    Description = "Show all capabilities"
                //},
                //new Capability()
                //{
                //    HashTag = "#uptime",
                //    Description = "Show how long bot has been running"
                //},
                //new Capability()
                //{
                //    HashTag = "#fbk",
                //    Description = "Adds FBK logo to your profile picture"
                //},
                new Capability()
                {
                    HashTag = "#referee",
                    Description = "Which SHL referee are you? Tweet to find out!"
                }
            };
        }
    }
}
