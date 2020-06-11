using System;
using System.Collections.Generic;
using System.Linq;
using jBot.Lib.Models;
using TweetSharp;

namespace jBot.Lib
{
    public class Search
    {
        private readonly ServiceInstance _serviceInstance;
        private DataStorage _dataFile;

        public Search(ServiceInstance serviceInstance, DataStorage dataFile)
        {
            _serviceInstance = serviceInstance;
            _dataFile = dataFile;
        }

        public List<TwitterStatus> SearchTweets(SearchParams searchParams)
        {
            var searchOptions = new SearchOptions();

            if (searchParams.HashTags.Count > 0)
                searchOptions.Q = string.Join(' ', searchParams.HashTags);

            searchOptions.SinceId = _dataFile.LastSinceID;

            if (searchParams.MaxItems > 0)
                searchOptions.Count = searchParams.MaxItems;

            var result = _serviceInstance.Instance.Search(searchOptions);

            return result.Statuses.ToList();
        }
    }
}
