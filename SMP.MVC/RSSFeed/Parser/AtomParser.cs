namespace SMP.MVC.RSSFeed.Parser
{
    using System.Xml.Linq;
    using Feeds;

    internal class AtomParser : AbstractXmlFeedParser
    {
        public override BaseFeed Parse(string feedXml, XDocument feedDoc)
        {
            AtomFeed feed = new AtomFeed(feedXml, feedDoc.Root);
            return feed;
        }
    }
}
