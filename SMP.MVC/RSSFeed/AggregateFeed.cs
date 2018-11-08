using SMP.MVC.RSSFeed.Feeds;
using System;
using System.Collections.Generic;

namespace SMP.MVC.RSSFeed
{
    public class AggregateFeed
    {
        public string Title { get; set; }

        public string Link { get; set; }

        public string FeedLink { get; set; }

        public string FeedTitle { get; set; }

        public string Description { get; set; }

        public string PublishingDateString { get; set; }

        public DateTime? PublishingDate { get; set; }

        public string Author { get; set; }

        public string Id { get; set; }

        public ICollection<string> Categories { get; set; }

        public string Content { get; set; }

        public BaseFeedItem SpecificItem { get; set; }

        public AggregateFeed()
        {
        }

        public AggregateFeed(BaseFeedItem feedItem)
        {
            this.Title = feedItem.Title;
            this.Link = feedItem.Link;
            this.Categories = new List<string>();
            this.SpecificItem = feedItem;
        }
    }
}
