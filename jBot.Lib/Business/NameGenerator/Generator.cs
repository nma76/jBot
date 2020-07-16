using System.Collections.Generic;
using jBot.Lib.Business.Extension;
using jBot.Lib.Models;

namespace jBot.Lib.Business.NameGenerator
{
    public class Generator
    {
        private readonly List<Person> _nameList;

        public string OriginalName { get; set; }
        public Person Referee
        {
            get
            {
                var namePos = OriginalName.ToIndexNumber(_nameList.Count - 1);
                return _nameList[namePos];
            }
        }

        public Generator(List<Person> nameList)
        {
            _nameList = nameList;
        }
    }
}
