using Services;

namespace Wikirandom
{
    class Program
    {
        private static async Task<int> Main(string[] args)
        {
            Console.WriteLine("This app retrieves <title> tag content from random Wikipedia article. Press any key to continue...");
            Console.ReadKey();
            Console.WriteLine("\nPress 1 to use HttpClient,\nPress 2 to use HTMLAgilityPack\nPress any other key to exit: ");
            var appDriver = Console.ReadLine();
            //var appDriver = args[0];
            IGetWikiRandom screen = default;
            List<string> articlesList = new List<string>();

            if (appDriver == "1")
            {
                screen = new GetWikiRandomHttpClientService(); //DI here
            }
            else if (appDriver == "2")
            {
                screen = new GetWikiRandomHTMLAgilityPack(); //DI here
            }

            else
            {
                return 0;
            }
                
            var wikiReader = new WikiReader(screen);

            try
            {
                Console.WriteLine("How many article headers would you like to read?");
                int numberOfArticles = int.Parse(Console.ReadLine());

                for (int i = 0; i < numberOfArticles; i++)
                {
                    articlesList.Add(await wikiReader.LoadArticle());
                }

                //wikiReader.ReadAdHocArticle();
                //wikiReader.MySortingKey("k");
                wikiReader.ShowAllArticles(articlesList);
                wikiReader.ShowFavArticles(articlesList, 15);

            }
            catch (Exception e)
            {
                Console.WriteLine("Error occured! Contact your application supporter. Issue details: " + e);
            }

            Console.ReadKey();
            return 0;
        }
    }
}
