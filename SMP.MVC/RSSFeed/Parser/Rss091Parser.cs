namespace SMP.MVC.RSSFeed.Parser
{
    using System.Xml.Linq;
    using Feeds;

    internal class Rss091Parser : AbstractXmlFeedParser
    {
        public override BaseFeed Parse(string feedXml, XDocument feedDoc)
        {
            var rss = feedDoc.Root;
            var channel = rss.GetElement("channel");
            Rss091Feed feed = new Rss091Feed(feedXml, channel);
            return feed;
        }
    }
}
