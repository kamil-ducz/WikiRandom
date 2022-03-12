using HtmlAgilityPack;

namespace Services
{
    public class GetWikiRandomHTMLAgilityPack : IGetWikiRandom //interface forces async
    {
        public Task<string> GetWikiRandom()
        {
            var html = ConstDefinitions.URL;
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(html);
            var node = htmlDoc.DocumentNode.SelectSingleNode("//head/title");
            var title = node.OuterHtml;
            title = HelperMethods.GetBetween(title, "<title>", " - Wikipedia</title>");
            return Task.FromResult(title);
        }
    }
}