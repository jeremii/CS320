using SMP.MVC.RSSFeed.Feeds;

namespace SMP.MVC.RSSFeed.Parser
{
    internal interface IFeedParser
    {
        BaseFeed Parse(string feedXml);
    }
}
