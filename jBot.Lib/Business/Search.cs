using System;
using System.Collections.Generic;
using System.Linq;
using jBot.Lib.Models;
using TweetSharp;

namespace jBot.Lib.Business
{
    public class Search
    {
        private readonly ServiceInstance _serviceInstance;
        //private DataStorage _dataFile;

        //public Search(ServiceInstance serviceInstance, DataStorage dataFile)
        public Search(ServiceInstance serviceInstance)
        {
            _serviceInstance = serviceInstance;
            //_dataFile = dataFile;
        }

        public List<TwitterStatus> SearchTweets(SearchParams searchParams)
        {
            var searchOptions = new SearchOptions();

            if (searchParams.HashTags.Count > 0)
                searchOptions.Q = string.Join(' ', searchParams.HashTags);

            //searchOptions.SinceId = _dataFile.LastSinceID;

            if (searchParams.MaxItems > 0)
                searchOptions.Count = searchParams.MaxItems;

            var result = _serviceInstance.Instance.Search(searchOptions);

            return result.Statuses.ToList();
        }
    }
}
