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

        public Search(ServiceInstance serviceInstance)
        {
            _serviceInstance = serviceInstance;
        }

        public List<TwitterStatus> SearchTweets(SearchParams searchParams, bool excludeReplies = true)
        {
            var searchOptions = new SearchOptions();

            if (searchParams.HashTags.Count > 0)
                searchOptions.Q = string.Join(' ', searchParams.HashTags);

            searchOptions.SinceId = searchParams.SinceId;

            if (searchParams.MaxItems > 0)
                searchOptions.Count = searchParams.MaxItems;

            var result = _serviceInstance.Instance.Search(searchOptions);

            if (excludeReplies)
                return result.Statuses.Where(x => x.InReplyToStatusId.Equals(null)).ToList();
            else
                return result.Statuses.ToList();
        }
    }
}
