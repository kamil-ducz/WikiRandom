using Services;
using System;
using System.Threading.Tasks;

namespace Wikirandom
{
    class WikiReader
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        private readonly IGetWikiRandom Screen;
        public WikiReader(IGetWikiRandom screen)
        {
            Screen = screen;

        }
        public async Task ReadArticle()
        {
            var article = await Screen.GetWikiRandom();
            Console.WriteLine(article);
            
        }
    }
}
