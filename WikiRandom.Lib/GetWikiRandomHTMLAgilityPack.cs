using HtmlAgilityPack;
using System.Threading.Tasks;

//namespace Services
//{
//    public class GetWikiRandomHTMLAgilityPack : IGetWikiRandom //interface forces async
//    {
//        public Task<string> GetWikiRandom()
//        {
//            var html = ConstDefinitionsOld.URL;
//            HtmlWeb web = new HtmlWeb();
//            var htmlDoc = web.Load(html);
//            var node = htmlDoc.DocumentNode.SelectSingleNode("//head/title");
//            var title = node.OuterHtml;
//            title = HelperMethodsOld.GetBetween(title, "<title>", " - Wikipedia</title>");
//            return Task.FromResult(title);
//        }
//    }
//} 