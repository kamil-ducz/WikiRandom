using HtmlAgilityPack;
using Services;
using System.Threading.Tasks;

namespace WebApplication4.Services.Classes
{
    public class WikiServiceHTMLAgilityPack : IWikiService
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
